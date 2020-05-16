using System;
using MongoDB.Bson.Serialization.Attributes;

namespace bright.Data.Models
{
    [BsonIgnoreExtraElements]
    public class AppState
    {
        public DateTime? LastScanTime { get; set; }
        
        public int? SchemaVersion { get; set; }

        [BsonId] public string Id { get; set; }
    }
}