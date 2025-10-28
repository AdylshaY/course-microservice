namespace CourseMicroservice.Order.Application.Contracts.Repositories;

using CourseMicroservice.Order.Domain.Entities;

public interface IOrderRepository : IGenericRepository<Guid, Order>
{
    Task<List<Order>> GetOrderListByUserId(Guid buyerId);
    Task SetStatus(string orderCode, Guid paymentId, OrderStatus status);
}
