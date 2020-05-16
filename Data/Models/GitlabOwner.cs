using System;
using MongoDB.Bson.Serialization.Attributes;

namespace bright.Data.Models
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