using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByBlogId(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken);

    Task<IEnumerable<Post>> GetPostsByBlogSubscriptions(PagedFilter pagedFilter, IEnumerable<string> subscriptionsIds,
        CancellationToken cancellationToken);
}