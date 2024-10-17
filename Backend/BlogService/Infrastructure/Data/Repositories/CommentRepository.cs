using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Comments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CommentRepository(
    MongoDbContext context) 
    : RepositoryBase<Comment>(context), ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetCommentsByPostId(PagedFilter pagedFilter, string postId, CancellationToken cancellationToken)
    {
        var spec = new CommentsByPostIdSpecification(postId);
        
        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}