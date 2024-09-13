using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    AppDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository
{
    public async Task<Blog> GetBlogByIdAndUserId(Guid blogId, string userId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.Id == blogId && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);  
    }
}