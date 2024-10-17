using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Subscriptions;

public class SubscribersByBlogIdSpecification(
    string blogId)
    : Specification<Subscription>
{
    public override Expression<Func<Subscription, bool>> ToExpression()
    {
        return subscription => subscription.SubscribedAtId == blogId;
    }
}