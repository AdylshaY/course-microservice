using CourseMicroservice.Order.Application.Contracts.Repositories;

namespace CourseMicroservice.Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
{
}
