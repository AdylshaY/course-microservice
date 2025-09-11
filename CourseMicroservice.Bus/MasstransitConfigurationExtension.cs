using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseMicroservice.Bus
{
    public static class MasstransitConfigurationExtension
    {
        public static IServiceCollection AddMasstransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOptions)).Get<BusOptions>())!;

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                });
            });

            return services;
        }
    }
}
