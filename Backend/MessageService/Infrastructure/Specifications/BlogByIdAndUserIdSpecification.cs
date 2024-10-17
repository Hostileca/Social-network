using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class BlogByIdAndUserIdSpecification(
    Guid blogId, string userId)
    : Specification<Blog>
{
    public override Expression<Func<Blog, bool>> ToExpression()
    {
        return blog => blog.Id == blogId && blog.UserId == userId;
    }
}