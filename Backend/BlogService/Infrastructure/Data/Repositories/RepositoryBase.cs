using Domain.Repositories;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RepositoryBase<TEntity> 
    : IRepository<TEntity>
    where TEntity : class

{
    protected readonly MongoDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(MongoDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Find(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => specification.IsSatisfiedBy(e)).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }

    public void Delete(TEntity item)
    {
        _dbSet.Remove(item);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}