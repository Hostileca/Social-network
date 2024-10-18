using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Comments;

public class CommentsByPostIdSpecification(
    string postId) 
    : Specification<Comment>
{
    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return comment => comment.PostId == postId;
    }
}