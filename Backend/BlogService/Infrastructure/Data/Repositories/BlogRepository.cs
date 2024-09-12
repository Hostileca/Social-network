using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    MongoDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository
{
    public async Task<Blog> GetByIdAndUserIdAsync(string id, string userId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }
}