using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    AppDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository
{
    public async Task<Blog> GetBlogByIdAndUserIdAsync(Guid blogId, string userId, CancellationToken cancellationToken)
    {
        var spec = new BlogByIdAndUserIdSpecification(blogId, userId);
        
        return await _dbSet.Where(spec.ToExpression()).FirstOrDefaultAsync(cancellationToken);  
    }
}