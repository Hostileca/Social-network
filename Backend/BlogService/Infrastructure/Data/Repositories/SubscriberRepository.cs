using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class SubscriberRepository(
    MongoDbContext context) 
    : RepositoryBase<Subscription>(context), ISubscriberRepository
{
    public async Task<IEnumerable<Subscription>> GetBlogSubscribers(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken)
    {
        var spec = new SubscribersByBlogIdSpecification(blogId);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Subscription>> GetBlogSubscriptions(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken)
    {
        var spec = new SubscriptionsByBlogIdSpecification(blogId);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}