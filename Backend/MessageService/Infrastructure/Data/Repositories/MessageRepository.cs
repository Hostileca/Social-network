using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MessageRepository(
    AppDbContext context) 
    : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetChatMessages(int pageNumber, int pageSize, Guid chatId, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.ChatId == chatId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}