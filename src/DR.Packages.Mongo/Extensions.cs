using DR.Packages.Mongo.Models;
using DR.Packages.Mongo.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace DR.Packages.Mongo
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoContext(this IServiceCollection services, string connectionString, Action<IServiceCollection> config = null)
        {
            var mongoConnection = connectionString.Split(';')[0];
            var database = connectionString.Split(';')[1];

            services.AddSingleton(context =>
            {
                return new MongoClient(mongoConnection);
            });

            services.AddScoped(context =>
            {
                var client = context.GetService<MongoClient>();
                return client.GetDatabase(database);
            });

            config?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
            where TEntity : IIdentifiable
        {
            services.AddScoped<IMongoRepository<TEntity>>(ctx => new MongoRepository<TEntity>(ctx.GetService<IMongoDatabase>(), collectionName));
            return services;
        }
    }
}
