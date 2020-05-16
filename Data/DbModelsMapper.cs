using System;
using bright.Data.Models;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;

namespace bright.Data
{
    public class DbModelsMapper
    {
        public GitlabLinks Map(Links l)
        {
            return new GitlabLinks
            {
                Events = l.Events,
                Issues = l.Issues,
                Labels = l.Labels,
                Members = l.Members,
                Self = l.Self,
                MergeRequests = l.MergeRequests,
                RepoBranches = l.RepoBranches
            };
        }

        public GitlabProject Map(Project i)
        {
            return new GitlabProject
            {
                Id = i.Id,
                Name = i.Name,
                Path = i.Path,
                Links = Map(i.Links),
                Owner = Map(i.Owner),
                WebUrl = i.WebUrl,
                Archived = i.Archived,
                Namespace = Map(i.Namespace),
                AvatarUrl = i.AvatarUrl,
                CreatedAt = i.CreatedAt,
                CreatorId = i.CreatorId,
                StarCount = i.StarCount,
                ForksCount = i.ForksCount,
                Statistics = Map(i.Statistics),
                Visibility = Map(i.Visibility),
                PublicJobs = i.PublicJobs,
                WikiEnabled = i.WikiEnabled,
                JobsEnabled = i.JobsEnabled,
                Permissions = Map(i.Permissions),
                Description = i.Description,
                ImportError = i.ImportError,
                SshUrlToRepo = i.SshUrlToRepo,
                RunnersToken = i.RunnersToken,
                ImportStatus = i.ImportStatus,
                HttpUrlToRepo = i.HttpUrlToRepo,
                DefaultBranch = i.DefaultBranch,
                IssuesEnabled = i.IssuesEnabled,
                LastActivityAt = i.LastActivityAt,
                SnippetsEnabled = i.SnippetsEnabled,
                OpenIssuesCount = i.OpenIssuesCount,
                PathWithNamespace = i.PathWithNamespace,
                NameWithNamespace = i.NameWithNamespace,
                MergeRequestsEnabled = i.MergeRequestsEnabled,
                RequestAccessEnabled = i.RequestAccessEnabled,
                SharedRunnersEnabled = i.SharedRunnersEnabled,
                ContainerRegistryEnabled = i.ContainerRegistryEnabled,
                OnlyAllowMergeIfPipelineSucceeds = i.OnlyAllowMergeIfPipelineSucceeds,
                OnlyAllowMergeIfAllDiscussionsAreResolved = i.OnlyAllowMergeIfAllDiscussionsAreResolved
            };
        }

        private string Map(ProjectVisibilityLevel v)
        {
            return v.ToString().ToLowerInvariant();
        }

        private GitlabStatistics Map(Statistics s)
        {
            throw new NotImplementedException();
        }

        private GitlabPermissions Map(Permissions p)
        {
            return new GitlabPermissions
            {
                GroupAccess = Map(p.GroupAccess),
                ProjectAccess = Map(p.ProjectAccess)
            };
        }

        private GitlabAccess Map(Access a)
        {
            return new GitlabAccess
            {
                AccessLevel = a.AccessLevel,
                NotificationLevel = a.NotificationLevel
            };
        }

        private GitlabOwner Map(Owner o)
        {
            return new GitlabOwner
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt
            };
        }

        private GitlabNamespace Map(Namespace n)
        {
            return new GitlabNamespace
            {
                Id = n.Id,
                Kind = n.Kind,
                Name = n.Name,
                Path = n.Path,
                FullPath = n.FullPath
            };
        }
    }
}