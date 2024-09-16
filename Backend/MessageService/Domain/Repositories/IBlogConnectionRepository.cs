using Domain.Entities;

namespace Domain.Repositories;

public interface IBlogConnectionRepository : IRepository<BlogConnection>
{
    public Task<IEnumerable<BlogConnection>> GetConnectionsByBlogId(Guid blogId, CancellationToken cancellationToken);
}