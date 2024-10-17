using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class MessagesByChatIdSpecification(
    Guid chatId)
    : Specification<Message>
{
    public override Expression<Func<Message, bool>> ToExpression()
    {
        return message => message.ChatId == chatId;
    }
}