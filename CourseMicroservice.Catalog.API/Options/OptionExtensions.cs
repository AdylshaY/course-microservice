using Microsoft.Extensions.Options;

namespace CourseMicroservice.Catalog.API.Options
{
    public static class OptionExtensions
    {
        public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
        {
            services.AddOptions<MongoDbOptions>().BindConfiguration(nameof(MongoDbOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbOptions>>().Value);

            return services;
        }
    }
}
