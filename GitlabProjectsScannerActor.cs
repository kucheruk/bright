using System;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bright
{
    public class GitlabProjectsScannerActor : ReceiveActor
    {
        private readonly IOptions<AppConfig> _cfg;
        private readonly DbModelsMapper _dbMapper;
        private readonly AppStateGetQuery _getState;
        private readonly GitlabInstanceGate _gitlab;
        private readonly ILogger<GitlabProjectsScannerActor> _logger;
        private readonly AppStateSaveQuery _saveState;
        private ICancelable _cancellable;
        private IActorRef _scanners;

        public GitlabProjectsScannerActor(IOptions<AppConfig> cfg,
            AppStateGetQuery getState,
            AppStateSaveQuery saveState,
            GitlabInstanceGate gitlab,
            ILogger<GitlabProjectsScannerActor> logger,
            DbModelsMapper dbMapper)
        {
            _cfg = cfg;
            _getState = getState;
            _saveState = saveState;
            _gitlab = gitlab;
            _logger = logger;
            _dbMapper = dbMapper;
            ReceiveAsync<MsgTick>(RescanIfNeed);
        }

        private async Task RescanIfNeed(MsgTick arg)
        {
            var state = await _getState.GetAsync();
            var rescanDelay = _cfg.Value.GitlabScanInterval ?? Constants.DefaultGitlabScanInterval;
            if (state.LastScanTime + rescanDelay > DateTime.Now)
            {
                _logger.LogInformation("Time for new scan! {lastScan}", state.LastScanTime);
                var projects = await _gitlab.GetProjectsAsync();
                var projectsToStore = projects.Select(_dbMapper.Map);
                _logger.LogInformation("Got information about {count} projects from gitlab", projects.Count);
                foreach (var project in projectsToStore)
                {
                    _scanners.Tell(project);
                }

                state.LastScanTime = DateTime.Now;
                _logger.LogInformation("Saving new scan time to appstate");
                await _saveState.SaveAsync(state);
            }
        }

        protected override void PreStart()
        {
            _cancellable = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(60), Self, MsgTick.Instance, Self);
            StartScanners();
            base.PreStart();
        }

        private void StartScanners()
        {
            var props = new SmallestMailboxPool(10).Props(
                Context.DI().Props<ProjectInfoScanActor>());
            props.WithSupervisorStrategy(new OneForOneStrategy(2, 20, e => Directive.Restart));
            _scanners = Context.ActorOf(props, "scanner_worker");
        }

        protected override void PostStop()
        {
            _logger.LogWarning("Stopping");
            _cancellable.Cancel();
            base.PostStop();
        }
    }
}