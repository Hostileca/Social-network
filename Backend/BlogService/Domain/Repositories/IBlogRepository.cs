using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IBlogRepository : IRepository<Blog>
{
    Task<Blog> GetByIdAndUserIdAsync(string id, string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Blog>> GetBlogsByUserId(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Blog>> GetBlogsByFilter(PagedFilter pagedFilter, BlogFilter filter, CancellationToken cancellationToken);
}