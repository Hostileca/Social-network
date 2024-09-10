using Domain.Entities;

namespace Application.Specifications.Implementations;

public class UserBlogsSpecification(
    string userId) : Specification<Blog>
{
    public override Func<Blog, bool> ToFunction()
    {
        return blog => blog.UserId == userId;
    }
}