using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Posts;

public class PostsByBlogSubscriptionsSpecification(
    IEnumerable<string> subscriptionsIds)
    : Specification<Post>
{
    public override Expression<Func<Post, bool>> ToExpression()
    {
        return post => subscriptionsIds.Contains(post.OwnerId);
    }
}