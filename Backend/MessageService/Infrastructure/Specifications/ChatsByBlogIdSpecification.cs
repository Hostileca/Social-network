using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class ChatsByBlogIdSpecification(
    Guid blogId) 
    : Specification<Chat>
{
    public override Expression<Func<Chat, bool>> ToExpression()
    {
        return chat => chat.Members.Any(member => member.BlogId == blogId);
    }
}