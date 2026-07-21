using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Play.Catalog.Service.Settings;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
namespace Play.Catalog.Service
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

            services.AddSingleton<IMongoDatabase>(serviceProvider =>
            {
                var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
                return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
            });

            services.AddSingleton<IRepository<Item>>(serviceProvider =>
            {
                var database = serviceProvider.GetRequiredService<IMongoDatabase>();
                return new MongoRepository(database, "items");
            });

            return services;

          
        }
    }
}
