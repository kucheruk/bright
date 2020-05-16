using System.Threading.Tasks;
using bright.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bright
{
    public class AppStateSaveQuery
    {
        private readonly IOptions<AppConfig> _cfg;
        private readonly MongoStore _ms;

        public AppStateSaveQuery(IOptions<AppConfig> cfg, MongoStore ms)
        {
            _cfg = cfg;
            _ms = ms;
        }

        public async Task SaveAsync(AppState state)
        {
            await _ms.App.ReplaceOneAsync(Builders<AppState>.Filter.Eq(a => a.Id, _cfg.Value.AppInstance), state);
        }
    }
}