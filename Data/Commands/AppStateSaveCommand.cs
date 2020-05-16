using System.Threading.Tasks;
using bright.Config;
using bright.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bright.Data.Commands
{
    public class AppStateSaveCommand
    {
        private readonly IOptions<AppConfig> _cfg;
        private readonly MongoStore _ms;

        public AppStateSaveCommand(IOptions<AppConfig> cfg, MongoStore ms)
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