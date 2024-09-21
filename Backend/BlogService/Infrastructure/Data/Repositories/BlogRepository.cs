using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    MongoDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository
{
    public async Task<Blog> GetByIdAndUserIdAsync(string id, string userId, CancellationToken cancellationToken)
    {
        var spec = new BlogByIdAndUserIdSpecification(id, userId);
        
        return await _dbSet.Where(spec.ToFunction()).AsQueryable().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Blog>> GetBlogsByUserId(string userId, CancellationToken cancellationToken)
    {
        var spec = new UserBlogsSpecification(userId);

        return await _dbSet.Where(spec.ToFunction()).AsQueryable().ToListAsync(cancellationToken);
    }
}