using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class ChatMembersByChatIdSpecification(
    Guid chatId) : Specification<ChatMember>
{
    public override Expression<Func<ChatMember, bool>> ToExpression()
    {
        return chatMember => chatMember.ChatId == chatId;
    }
}