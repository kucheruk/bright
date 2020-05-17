using Microsoft.Extensions.DependencyInjection;

namespace bright.Actors
{
    public static class ParsersExtensions
    {
        public static void AddFileParsers(this IServiceCollection services)
        {
            services.AddSingleton<IFileParser, CsprojFileParser>();
        }
    }
}