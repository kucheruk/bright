using System;

namespace bright
{
    public class AppConfig
    {
        public string GitlabToken { get; set; }
        public string MongoConnectionString { get; set; }
        public string GitlabUrl { get; set; }
        public string MongoDbSuffix { get; set; }
        public string AppInstance { get; set; } = "default";
        public TimeSpan? GitlabScanInterval { get; set; }
        public string ProjectDefinitionFileName { get; set; }
    }
}