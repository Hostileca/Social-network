using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface ISubscriberRepository : IRepository<Subscription>
{
    Task<IEnumerable<Subscription>> GetBlogSubscribers(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Subscription>> GetBlogSubscriptions(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken);
}