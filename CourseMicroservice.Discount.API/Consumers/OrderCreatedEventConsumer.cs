using CourseMicroservice.Bus.Events;
using CourseMicroservice.Discount.API.Features.Discounts;
using CourseMicroservice.Discount.API.Repositories;

namespace CourseMicroservice.Discount.API.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var discount = new DiscountEntity
            {
                Id = NewId.NextSequentialGuid(),
                Code = DiscountCodeGenerator.Generate(),
                Rate = 0.1f,
                UserId = context.Message.UserId,
                Expired = DateTime.Now.AddMonths(1),
                Created = DateTime.Now,
            };

            await appDbContext.Discounts.AddAsync(discount);
            await appDbContext.SaveChangesAsync();
        }
    }
}
