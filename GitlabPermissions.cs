using GitLabApiClient.Models.Projects.Responses;

namespace bright
{
    public class GitlabPermissions
    {
        public GitlabAccess GroupAccess { get; set; }

        public GitlabAccess ProjectAccess { get; set; }

    }
}