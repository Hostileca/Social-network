using Domain.Entities;

namespace Domain.Repositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<Blog> GetByIdAndUserIdAsync(string id, string userId, CancellationToken cancellationToken);
}