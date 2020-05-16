using Microsoft.Extensions.DependencyInjection;

namespace bright
{
    public static class GitlabExtensions
    {
        public static void AddGitlab(this IServiceCollection services)
        {
            services.AddSingleton<GitlabInstanceGate>();
        }
    }
}