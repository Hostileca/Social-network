using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BlogConnectionRepository(
    AppDbContext context)
    : RepositoryBase<BlogConnection>(context), IBlogConnectionRepository
{
    public async Task<IEnumerable<BlogConnection>> GetConnectionsByBlogId(Guid blogId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.BlogId == blogId).ToListAsync(cancellationToken);
    }
}