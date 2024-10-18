using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications.Blogs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    MongoDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository
{
    public async Task<Blog> GetByIdAndUserIdAsync(string id, string userId, CancellationToken cancellationToken)
    {
        var specification = new BlogByIdAndUserIdSpecification(id, userId);
        
        return await _dbSet.Where(specification.ToExpression()).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Blog>> GetBlogsByUserId(string userId, CancellationToken cancellationToken)
    {
        var specification = new UserBlogsSpecification(userId);

        return await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Blog>> GetBlogsByFilter(PagedFilter pagedFilter, BlogFilter filter, CancellationToken cancellationToken)
    {
        var spec = new BlogsByFilterSpecification(filter);
        
        return await _dbSet
            .Where(spec.ToExpression())
            .Paged(pagedFilter)
            .ToListAsync(cancellationToken);
    }
}