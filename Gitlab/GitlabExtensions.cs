using Microsoft.Extensions.DependencyInjection;

namespace bright.Gitlab
{
    public static class GitlabExtensions
    {
        public static void AddGitlab(this IServiceCollection services)
        {
            services.AddSingleton<GitlabInstanceGate>();
        }
    }
}