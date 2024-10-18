using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IChatMemberRepository : IRepository<ChatMember>
{
    Task<IEnumerable<ChatMember>> GetChatMembersByChatIdAsync(PagedFilter pagedFilter, Guid chatId, CancellationToken cancellationToken);
}