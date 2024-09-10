using Application.Specifications.Interfaces;

namespace Application.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification);
    Task AddAsync(TEntity item, CancellationToken cancellationToken = default);
    void Delete(TEntity item);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}