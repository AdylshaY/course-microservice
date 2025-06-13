using CourseMicroservice.Order.Domain.Entities;
using System.Linq.Expressions;

namespace CourseMicroservice.Order.Application.Contracts.Repositories;

public interface IGenericRepository<TId, TEntity> where TEntity : BaseEntity<TId> where TId : struct
{
    public Task<bool> AnyAsync(TId id);

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    public Task<List<TEntity>> GetAllAsync();

    public Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);

    ValueTask<TEntity?> GetByIdAsync(TId id);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
