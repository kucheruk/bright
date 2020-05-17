using Microsoft.Extensions.DependencyInjection;

namespace bright.Actors
{
    public static class ProjectMetaExtensios
    {
        public static void AddProjectMeta(this IServiceCollection services)
        {
            services.AddSingleton<ProjectMetaSaveCommand>();
        }
    }
}