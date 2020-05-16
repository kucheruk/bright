using bright.Config;
using bright.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bright.Data
{
    public class MongoStore
    {
        private readonly MongoClient _mc;

        public MongoStore(IOptions<AppConfig> cfg)
        {
            _mc = new MongoClient(cfg.Value.MongoConnectionString);
            Db = _mc.GetDatabase("bright" + cfg.Value.MongoDbSuffix);
            Projects = GetCollection<GitlabProject>("projects");
            App = GetCollection<AppState>("appState");
        }

        public IMongoDatabase Db { get; set; }

        private IMongoCollection<T> GetCollection<T>(string col) => Db.GetCollection<T>(col);

        public IMongoCollection<GitlabProject> Projects { get; }
        public IMongoCollection<AppState> App { get; set; }
    }
}