using CourseMicroservice.Order.Application.Contracts.Repositories;
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
}
