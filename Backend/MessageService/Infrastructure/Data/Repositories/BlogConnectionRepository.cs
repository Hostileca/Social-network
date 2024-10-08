using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BlogConnectionRepository(
    AppDbContext context)
    : RepositoryBase<BlogConnection>(context), IBlogConnectionRepository
{
    public async Task<IEnumerable<BlogConnection>> GetConnectionsByBlogId(Guid blogId, CancellationToken cancellationToken)
    {
        var spec = new ConnectionsByBlogIdSpecification(blogId);
        
        return await _dbSet.Where(spec.ToExpression()).ToListAsync(cancellationToken);
    }
}