using Domain.Entities;

namespace Domain.Repositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<Blog> GetBlogByIdAndUserIdAsync(Guid blogId, string userId, CancellationToken cancellationToken);
}