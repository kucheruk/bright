using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Akka.Actor;
using bright.Config;
using bright.Data.Models;
using bright.Gitlab;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Files.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Trees.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace bright.Actors
{
    public class ProjectInfoScanActor : ReceiveActor
    {
        private readonly GitlabInstanceGate _gitlab;
        private readonly IOptions<AppConfig> _cfg;
        private readonly ProjectMetaSaveCommand _save;
        private readonly IEnumerable<IFileParser> _parsers;
        private readonly ILogger<ProjectInfoScanActor> _psa;

        public ProjectInfoScanActor(GitlabInstanceGate gitlab, 
            IOptions<AppConfig> cfg, 
            ProjectMetaSaveCommand save,
            IEnumerable<IFileParser> parsers,
            ILogger<ProjectInfoScanActor> psa)
        {
            _gitlab = gitlab;
            _cfg = cfg;
            _save = save;
            _parsers = parsers;
            _psa = psa;
            ReceiveAsync<GitlabProject>(ScanProject);
        }

        private async Task ScanProject(GitlabProject arg)
        {
            var tree = await _gitlab.GetProjectTree(arg.Id);
            await TraverseTreeRetrieveInfoAsync(tree, arg.Id);
        }

        private async Task TraverseTreeRetrieveInfoAsync(IList<Tree> tree, ProjectId pid)
        {
            foreach (var t in tree)
            {
                foreach (var parser in _parsers)
                {
                    if (parser.Match(t.Path))
                    {
                        var data = await parser.RetrieveDataAsync(t.Path, pid);
                        await _save.SaveAsync(data);
                    }
                }
            }
        }
    }
}