using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class ReactionsByMessageIdSpecification(
    Guid messageId)
    : Specification<Reaction>
{
    public override Expression<Func<Reaction, bool>> ToExpression()
    {
        return reaction => reaction.MessageId == messageId; 
    }
}