using System.Threading.Tasks;
using bright.Config;
using bright.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bright.Data.Queries
{
    public class AppStateGetQuery
    {
        private readonly MongoStore _ms;
        private readonly IOptions<AppConfig> _cfg;

        public AppStateGetQuery(MongoStore ms, IOptions<AppConfig> cfg)
        {
            _ms = ms;
            _cfg = cfg;
        }
        public async Task<AppState> GetAsync()
        {
            var state = await _ms.App.Find(a => a.Id == _cfg.Value.AppInstance).FirstOrDefaultAsync();
            return state ?? new AppState
            {
                Id = _cfg.Value.AppInstance
            };
        }
    }
}