using Domain.Specifications;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken);
    Task AddAsync(TEntity item, CancellationToken cancellationToken = default);
    void Delete(TEntity item);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}