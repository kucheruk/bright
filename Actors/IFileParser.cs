using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;

namespace bright.Actors
{
    public interface IFileParser
    {
        bool Match(string f);
        Task<ProjectMetaData> RetrieveDataAsync(string path, ProjectId pid);
    }
}