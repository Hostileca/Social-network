using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    public Task<IEnumerable<Message>> GetChatMessages(PagedFilter pagedFilter, Guid chatId, CancellationToken cancellationToken);
}