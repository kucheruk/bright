using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace bright.Data.Models
{
    public class GitlabProject
    {
        public string LastActivityAt { get; set; }

        public string Description { get; set; }

        public bool ContainerRegistryEnabled { get; set; }

        public bool Archived { get; set; }

        public GitlabLinks Links { get; set; }

        public string AvatarUrl { get; set; }

        public int CreatorId { get; set; }

        public string CreatedAt { get; set; }

        public string DefaultBranch { get; set; }

        public string ImportError { get; set; }

        public string HttpUrlToRepo { get; set; }

        public int ForksCount { get; set; }

        [BsonId]
        public int Id { get; set; }

        public bool IssuesEnabled { get; set; }

        public string ImportStatus { get; set; }

        public bool JobsEnabled { get; set; }

        public GitlabOwner Owner { get; set; }

        public GitlabNamespace Namespace { get; set; }

        public string Name { get; set; }

        public bool MergeRequestsEnabled { get; set; }

        public string NameWithNamespace { get; set; }

        public bool? OnlyAllowMergeIfPipelineSucceeds { get; set; }

        public bool? OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        public int OpenIssuesCount { get; set; }

        public bool PublicJobs { get; set; }

        public string PathWithNamespace { get; set; }

        public string Path { get; set; }

        public GitlabPermissions Permissions { get; set; }

        public string RunnersToken { get; set; }

        public bool RequestAccessEnabled { get; set; }

        public bool SharedRunnersEnabled { get; set; }

        public GitlabStatistics Statistics { get; set; }

        public string SshUrlToRepo { get; set; }

        public bool SnippetsEnabled { get; set; }

        public int StarCount { get; set; }

        public string Visibility { get; set; }

        public List<string> TagList { get; } = new List<string>();

        public string WebUrl { get; set; }

        public bool WikiEnabled { get; set; }

    }
}