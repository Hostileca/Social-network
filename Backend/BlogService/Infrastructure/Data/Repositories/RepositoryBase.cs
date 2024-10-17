using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RepositoryBase<TEntity> 
    : IRepository<TEntity>
    where TEntity : EntityBase

{
    protected readonly MongoDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(MongoDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(PagedFilter pagedFilter, CancellationToken cancellationToken = default)
    {
        return await GetPaged(pagedFilter)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
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
    
    protected IQueryable<TEntity> GetPaged(PagedFilter pagedFilter)
    {
        return _dbSet
            .Skip((pagedFilter.PageNumber - 1) * pagedFilter.PageSize)
            .Take(pagedFilter.PageSize);
    }
}