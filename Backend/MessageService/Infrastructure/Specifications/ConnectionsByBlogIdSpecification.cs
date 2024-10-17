using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications;

public class ConnectionsByBlogIdSpecification(
    Guid blogId)
    : Specification<BlogConnection>
{
    public override Expression<Func<BlogConnection, bool>> ToExpression()
    {
        return connection => connection.BlogId == blogId;
    }
}