using System;
using MongoDB.Bson.Serialization.Attributes;

namespace bright
{
    [BsonIgnoreExtraElements]
    public class GitlabOwner
    {
        [BsonId]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }
    }
}