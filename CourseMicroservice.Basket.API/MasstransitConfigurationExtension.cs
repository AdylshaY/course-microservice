using CourseMicroservice.Basket.API.Consumers;
using CourseMicroservice.Bus;
using MassTransit;

namespace CourseMicroservice.Basket.API
{
    public static class MasstransitConfigurationExtension
    {
        public static IServiceCollection AddMasstransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOptions)).Get<BusOptions>())!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("basket-microservice.order-created.queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
