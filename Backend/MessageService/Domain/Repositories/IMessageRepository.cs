using Domain.Entities;

namespace Domain.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    public Task<IEnumerable<Message>> GetChatMessages(int pageNumber, int pageSize, Guid chatId, CancellationToken cancellationToken);
}