using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ChatRepository(
    AppDbContext context) 
    : RepositoryBase<Chat>(context), IChatRepository
{
    public async Task<IEnumerable<Chat>> GetChatsByBlogId(PagedFilter filter, Guid blogId, CancellationToken cancellationToken)
    {
        var spec = new ChatsByBlogIdSpecification(blogId);

        return await _dbSet
            .Where(spec.ToExpression())
            .Paged(filter)
            .ToListAsync(cancellationToken);
    }
}