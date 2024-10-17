using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Likes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class LikeRepository(
    MongoDbContext context) 
    : RepositoryBase<Like>(context), ILikeRepository
{
    public async Task<IEnumerable<Like>> GetLikeSendersByPostIdAsync(PagedFilter pagedFilter, string postId, 
        CancellationToken cancellationToken)
    {
        var spec = new LikeSendersByPostIdSpecification(postId);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Like>> GetBlogLikesAsync(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken)
    {
        var spec = new LikesBySenderIdSpecification(blogId);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}