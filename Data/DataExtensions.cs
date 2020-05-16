using Microsoft.Extensions.DependencyInjection;

namespace bright.Data
{
    public static class DataExtensions
    {
        public static void AddMongoStorage(this IServiceCollection services)
        {
            services.AddSingleton<MongoStore>();
            services.AddSingleton<DbModelsMapper>();
        }
    }
}