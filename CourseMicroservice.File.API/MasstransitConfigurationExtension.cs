using CourseMicroservice.Bus;
using CourseMicroservice.File.API.Consumer;
using MassTransit;

namespace CourseMicroservice.File.API
{
    public static class MasstransitConfigurationExtension
    {
        public static IServiceCollection AddMasstransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOptions)).Get<BusOptions>())!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<UploadCoursePictureCommandConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                    {
                        e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
