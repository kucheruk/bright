using MongoDB.Bson.Serialization.Attributes;

namespace bright
{
    [BsonIgnoreExtraElements]
    public class GitlabStatistics
    {
        public int JobArtifactsSize { get; set; }

        public int RepositorySize { get; set; }

        public int CommitCount { get; set; }

        public int LfsObjectsSize { get; set; }

        public int StorageSize { get; set; }

    }
}