using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications.Posts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class PostRepository(
    MongoDbContext context)
    : RepositoryBase<Post>(context), IPostRepository
{
    public async Task<IEnumerable<Post>> GetPostsByBlogId(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken)
    {
        var spec = new PostsByBlogIdSpecification(blogId);
        
        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Post>> GetPostsByBlogSubscriptions(PagedFilter pagedFilter, IEnumerable<string> subscriptionsIds,
        CancellationToken cancellationToken)
    {
        var spec = new PostsByBlogSubscriptionsSpecification(subscriptionsIds);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}