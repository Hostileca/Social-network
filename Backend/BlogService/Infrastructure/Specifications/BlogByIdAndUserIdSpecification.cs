using Domain.Entities;
using Domain.Specifications;

namespace Infrastructure.Specifications;

public class BlogByIdAndUserIdSpecification(
    string blogId, string userId) 
    : Specification<Blog>
{
    public override Func<Blog, bool> ToFunction()
    {
        return blog => blog.UserId == userId && blog.Id == blogId;
    }
}