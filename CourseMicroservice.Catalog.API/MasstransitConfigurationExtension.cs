using CourseMicroservice.Bus;
using CourseMicroservice.Catalog.API.Consumers;

namespace CourseMicroservice.Catalog.API
{
    public static class MasstransitConfigurationExtension
    {
        public static IServiceCollection AddMasstransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOptions)).Get<BusOptions>())!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CoursePictureUploadedEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue", e =>
                    {
                        e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
