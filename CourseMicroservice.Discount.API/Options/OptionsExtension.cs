using Microsoft.Extensions.Options;

namespace CourseMicroservice.Discount.API.Options
{
    public static class OptionsExtension
    {
        public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);

            return services;
        }
    }
}
