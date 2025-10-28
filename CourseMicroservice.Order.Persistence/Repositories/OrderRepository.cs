using CourseMicroservice.Order.Application.Contracts.Repositories;
using CourseMicroservice.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseMicroservice.Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
{
    public Task<List<Domain.Entities.Order>> GetOrderListByUserId(Guid buyerId)
    {
        return context.Orders.Include(x => x.OrderItemList)
            .Where(x => x.BuyerId == buyerId)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }

    public async Task SetStatus(string orderCode, Guid paymentId, OrderStatus status)
    {
        var order = await context.Orders.FirstAsync(x => x.Code.Equals(orderCode));
        order.Status = status;
        order.PaymentId = paymentId;
        context.Update(order);
    }
}
