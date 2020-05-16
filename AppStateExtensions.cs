using bright.Data.Commands;
using bright.Data.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace bright
{
    public static class AppStateExtensions
    {
        public static void AddAppState(this IServiceCollection services)
        {
            services.AddSingleton<AppStateGetQuery>();
            services.AddSingleton<AppStateSaveCommand>();
        }
    }
}