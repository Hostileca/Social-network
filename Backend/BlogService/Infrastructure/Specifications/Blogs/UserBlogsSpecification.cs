using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Blogs;

public class UserBlogsSpecification(
    string userId) : Specification<Blog>
{
    public override Expression<Func<Blog, bool>> ToExpression()
    {
        return blog => blog.UserId == userId;
    }
}