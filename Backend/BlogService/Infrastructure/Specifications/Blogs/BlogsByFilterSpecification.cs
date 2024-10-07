using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filters;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Blogs;

public class BlogsByFilterSpecification(
    BlogFilter blogFilter)
    : Specification<Blog>
{
    public override Expression<Func<Blog, bool>> ToExpression()
    {
        return blog =>
            (string.IsNullOrEmpty(blogFilter.Username) || blog.Username.Contains(blogFilter.Username)) &&
            
            (blogFilter.MinimalAge == 0 || GetAge(blog.DateOfBirth) >= blogFilter.MinimalAge) &&
            (blogFilter.MaximumAge == 0 || GetAge(blog.DateOfBirth) <= blogFilter.MaximumAge) &&
            
            (blogFilter.MinimalSubscribersCount == 0 || blog.Subscribers.Count() >= blogFilter.MinimalSubscribersCount) &&
            (blogFilter.MaximumSubscribersCount == 0 || blog.Subscribers.Count() <= blogFilter.MaximumSubscribersCount) &&
            
            (blogFilter.MinimalPostsCount == 0 || blog.Posts.Count() >= blogFilter.MinimalPostsCount) &&
            (blogFilter.MaximumPostsCount == 0 || blog.Posts.Count() <= blogFilter.MaximumPostsCount);

    }
    
    private int GetAge(DateTime dateOfBirth)
    {
        if (dateOfBirth == DateTime.MinValue)
        {
            return 0;
        }

        var today = DateTime.Today; 
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
