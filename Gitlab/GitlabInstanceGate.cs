using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using bright.Config;
using GitLabApiClient;
using GitLabApiClient.Models.Files.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using Microsoft.Extensions.Options;

namespace bright.Gitlab
{
    public class GitlabInstanceGate
    {
        private readonly GitLabClient _gc;

        public GitlabInstanceGate(IOptions<AppConfig> cfg)
        {
            _gc = new GitLabClient(cfg.Value.GitlabUrl, cfg.Value.GitlabToken);
        }

        public async Task<IList<Project>> GetProjectsAsync()
        {
            return await _gc.Projects.GetAsync(a =>
            {
                a.Archived = false;
                a.Order = ProjectsOrder.LastActivityAt;
                a.IncludeStatistics = true;
            });
        }

        public async Task<File> FindFile(int projectId, string filePath)
        {
            try
            {
                return await _gc.Files.GetAsync(projectId, filePath);
            }
            catch (GitLabException g)
            {
                if (g.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }
    }
}