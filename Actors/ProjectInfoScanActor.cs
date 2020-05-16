using System.Threading.Tasks;
using Akka.Actor;
using bright.Config;
using bright.Data.Models;
using bright.Gitlab;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace bright.Actors
{
    public class ProjectInfoScanActor : ReceiveActor
    {
        private readonly GitlabInstanceGate _gitlab;
        private readonly IOptions<AppConfig> _cfg;
        private readonly ILogger<ProjectInfoScanActor> _psa;

        public ProjectInfoScanActor(GitlabInstanceGate gitlab, IOptions<AppConfig> cfg, ILogger<ProjectInfoScanActor> psa)
        {
            _gitlab = gitlab;
            _cfg = cfg;
            _psa = psa;
            ReceiveAsync<GitlabProject>(ScanProject);
        }

        private async Task ScanProject(GitlabProject arg)
        {
            var file = await _gitlab.FindFile(arg.Id, _cfg.Value.ProjectDefinitionFileName);
            _psa.LogInformation("Got file info? {File}", JsonConvert.SerializeObject(file));
        }
    }
}