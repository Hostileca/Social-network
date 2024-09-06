using Application.Specifications.Interfaces;
using MongoDB.Driver;

namespace Application.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    IEnumerable<TEntity> Find(ISpecification<TEntity> specification);
    Task AddAsync(TEntity item, CancellationToken cancellationToken = default);
    void Delete(TEntity item);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}