using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MessageRepository(
    AppDbContext context) 
    : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetChatMessages(PagedFilter pagedFilter, Guid chatId, CancellationToken cancellationToken)
    {
        var spec = new MessagesByChatIdSpecification(chatId);
        
        return await _dbSet
            .Where(spec.ToExpression())
            .OrderByDescending(x => x.Date)
            .Paged(pagedFilter)
            .ToListAsync(cancellationToken);
    }
}