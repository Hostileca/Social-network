using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ChatMemberRepository(
    AppDbContext context) 
    : RepositoryBase<ChatMember>(context), IChatMemberRepository
{
    public async Task<IEnumerable<ChatMember>> GetChatMembersByChatIdAsync(PagedFilter pagedFilter, Guid chatId, CancellationToken cancellationToken)
    {
        var spec = new ChatMembersByChatIdSpecification(chatId);

        return await GetPaged(pagedFilter)
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}