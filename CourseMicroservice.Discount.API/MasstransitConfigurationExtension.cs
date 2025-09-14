using CourseMicroservice.Bus;
using CourseMicroservice.Discount.API.Consumers;

namespace CourseMicroservice.Discount.API
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

                    cfg.ReceiveEndpoint("discount-microservice.order-created.queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
