using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class BlogByIdAndUserIdSpecification(
    string blogId, string userId) 
    : Specification<Blog>
{
    public override Expression<Func<Blog, bool>> ToExpression()
    {
        return blog => blog.UserId == userId && blog.Id == blogId;
    }
}