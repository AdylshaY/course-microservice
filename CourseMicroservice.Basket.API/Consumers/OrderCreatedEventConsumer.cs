using CourseMicroservice.Bus.Events;
using MassTransit;
using CourseMicroservice.Basket.API.Features.Baskets;

namespace CourseMicroservice.Basket.API.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
            await basketService.DeleteBasket(context.Message.UserId);
        }
    }
}
