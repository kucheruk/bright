using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace bright.Actors
{
    [BsonIgnoreExtraElements]
    public class ProjectMetaData
    {
        [BsonId]
        public string Id { get; set; }
        public string Framework { get; set; }
        public List<ProjectDependencyInfo> Deps { get; set; }
    }
}