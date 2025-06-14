namespace CourseMicroservice.Order.Application.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
{
}
