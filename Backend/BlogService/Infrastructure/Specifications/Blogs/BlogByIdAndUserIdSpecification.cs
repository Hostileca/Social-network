using System.Linq.Expressions;
using SharedResources.Specifications;
using Domain.Entities;

namespace Infrastructure.Specifications.Blogs;

public class BlogByIdAndUserIdSpecification(
    string blogId, string userId) 
    : Specification<Blog>
{
    public override Expression<Func<Blog, bool>> ToExpression()
    {
        return blog => blog.UserId == userId && blog.Id == blogId;
    }
}