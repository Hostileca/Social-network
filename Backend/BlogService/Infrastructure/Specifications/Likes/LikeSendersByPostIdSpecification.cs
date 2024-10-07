using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Likes;

public class LikeSendersByPostIdSpecification(
    string postId)
    : Specification<Like>
{
    public override Expression<Func<Like, bool>> ToExpression()
    {
        return like => like.PostId == postId;
    }
}