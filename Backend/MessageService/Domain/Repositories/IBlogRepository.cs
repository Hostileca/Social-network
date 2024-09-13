using Domain.Entities;

namespace Domain.Repositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<Blog> GetBlogByIdAndUserId(Guid blogId, string userId, CancellationToken cancellationToken);
}