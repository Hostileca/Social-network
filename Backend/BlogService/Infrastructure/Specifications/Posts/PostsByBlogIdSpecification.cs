using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Posts;

public class PostsByBlogIdSpecification(
    string blogId)
    : Specification<Post>
{
    public override Expression<Func<Post, bool>> ToExpression()
    {
        return post => post.OwnerId == blogId;
    }
}