using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using bright.Config;
using GitLabApiClient;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Files.Responses;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Trees.Responses;
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

        public async Task<IList<Tree>> GetProjectTree(int argId)
        {
            var tree = await _gc.Trees.GetAsync(argId, a =>
            {
                a.Recursive = true;
            });
            return tree;
        }

        public async Task<File> GetFileContentAsync(ProjectId pid, string fullPath)
        {
            return await _gc.Files.GetAsync(pid, fullPath);
        }
    }
}