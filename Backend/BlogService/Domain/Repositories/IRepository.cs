using Domain.Specifications;
using MongoDB.Bson;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Find(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity item, CancellationToken cancellationToken = default);
    void Delete(TEntity item);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}