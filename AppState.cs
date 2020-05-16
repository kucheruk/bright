using System;
using MongoDB.Bson.Serialization.Attributes;

namespace bright
{
    [BsonIgnoreExtraElements]
    public class AppState
    {
        public DateTime? LastScanTime { get; set; }
        
        public int? SchemaVersion { get; set; }

        [BsonId] public string Id { get; set; }
    }
}