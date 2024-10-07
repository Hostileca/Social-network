using System.Collections;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface ILikeRepository : IRepository<Like>
{
    Task<IEnumerable<Like>> GetLikeSendersByPostIdAsync(PagedFilter pagedFilter, string postId, 
        CancellationToken cancellationToken);
    
    Task<IEnumerable<Like>> GetBlogLikesAsync(PagedFilter pagedFilter, string blogId, CancellationToken cancellationToken);
}