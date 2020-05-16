using Microsoft.Extensions.DependencyInjection;

namespace bright
{
    public static class AppStateExtensions
    {
        public static void AddAppState(this IServiceCollection services)
        {
            services.AddSingleton<AppStateGetQuery>();
            services.AddSingleton<AppStateSaveQuery>();
        }
    }
}