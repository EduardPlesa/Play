using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Repositories
{
    public static class RepoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceSettings = configuration
                .GetSection(nameof(ServiceSettings))
                .Get<ServiceSettings>();

            var mongoDbSettings = configuration
                .GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>();

            services.AddSingleton(serviceSettings ?? throw new InvalidOperationException("ServiceSettings configuration is missing."));
            services.AddSingleton(mongoDbSettings ?? throw new InvalidOperationException("MongoDbSettings configuration is missing."));
            services.AddSingleton<IMongoClient>(
                new MongoClient(mongoDbSettings.ConnectionString));
            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {

                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });
            {
                return services;
            }
        }
    }
}