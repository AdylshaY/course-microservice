using CourseMicroservice.Catalog.API.Options;
using MongoDB.Driver;

namespace CourseMicroservice.Catalog.API.Repositories
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddDatabaseServiceExtension(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoDbOptions>();

                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });

            return services;
        }
    }
}
