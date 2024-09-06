using Application.Specifications.Interfaces;
using Domain.Entities;

namespace Application.Specifications.Common;

public class BlogSubscribersSpecification(
    string blogId) : ISpecification<Blog>
{
    public Func<Blog, bool> ToFunction()
    {
        return blog => blog.Subscribtions.Any(x => x.SubscribedAtId == blogId);
    }
}