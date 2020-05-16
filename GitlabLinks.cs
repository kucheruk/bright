using MongoDB.Bson.Serialization.Attributes;

namespace bright
{
    [BsonIgnoreExtraElements]
    public class GitlabLinks
    {
        public string Members { get; set; }

        public string Issues { get; set; }

        public string Events { get; set; }

        public string Labels { get; set; }

        public string RepoBranches { get; set; }

        public string MergeRequests { get; set; }

        public string Self { get; set; }

    }
}