using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MessageRepository(
    AppDbContext context) 
    : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetChatMessages(PagedFilter pagedFilter, Guid chatId, CancellationToken cancellationToken)
    {
        return await GetPaged(pagedFilter)
            .ToListAsync(cancellationToken);
    }
}