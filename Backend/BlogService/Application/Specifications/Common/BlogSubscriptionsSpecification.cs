using Application.Specifications.Interfaces;
using Domain.Entities;

namespace Application.Specifications.Common;

public class BlogSubscriptionsSpecification(
    string blogId) : ISpecification<Blog>
{
    public Func<Blog, bool> ToFunction()
    {
        return blog => blog.Subscribers.Any(x => x.SubscribedById == blogId);
    }
}