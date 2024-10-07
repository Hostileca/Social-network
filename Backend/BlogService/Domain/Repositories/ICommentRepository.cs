using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface ICommentRepository : IRepository<Comment> 
{
    Task<IEnumerable<Comment>> GetCommentsByPostId(PagedFilter pagedFilter, string postId, CancellationToken cancellationToken);
}