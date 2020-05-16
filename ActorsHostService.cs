using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using GitLabApiClient;
using GitLabApiClient.Models.Projects.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bright
{
    public class ActorsHostService : IHostedService
    {
        private readonly ILogger<ActorsHostService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private ActorSystem _as;
        private MicrosoftDependencyResolver _resolver;
        private IActorRef _supervisor;

        public ActorsHostService(IServiceScopeFactory scopeFactory, ILogger<ActorsHostService> logger, IOptions<AppConfig > cfg)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            var gc = new GitLabClient(cfg.Value.GitlabUrl, cfg.Value.GitlabToken);
            var p = gc.Projects.GetAsync(a =>
            {
                a.Archived = false;
                a.Order = ProjectsOrder.LastActivityAt;
                a.IncludeStatistics = true;
            }).Result;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _as = ActorSystem.Create("bright", @"akka { stdout-loglevel = INFO
loglevel = DEBUG
log-config-on-start = on
loggers = [""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]
actor { debug { lifecycle = on
unhandled = on } } }");

            _resolver = new MicrosoftDependencyResolver(_scopeFactory, _as);
            _logger.LogInformation("Starting up with actor dependency resolver of {type}", _resolver.GetType());
            _supervisor = _as.ActorOf(_as.DI().Props<BrightSupervisor>()
                .WithSupervisorStrategy(SupervisorStrategy.DefaultStrategy));

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _as.Terminate();
        }
    }
}