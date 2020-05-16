using System.Threading.Tasks;
using bright.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bright
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