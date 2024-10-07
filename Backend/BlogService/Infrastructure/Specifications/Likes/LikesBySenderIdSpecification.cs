using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Likes;

public class LikesBySenderIdSpecification(
    string blogId)
    : Specification<Like>
{
    public override Expression<Func<Like, bool>> ToExpression()
    {
        return like => like.SenderId == blogId;
    }
}