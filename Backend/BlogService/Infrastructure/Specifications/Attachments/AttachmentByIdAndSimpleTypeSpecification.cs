using System.Linq.Expressions;
using Domain.Entities;
using SharedResources.Specifications;

namespace Infrastructure.Specifications.Attachments;

public class AttachmentByIdAndSimpleTypeSpecification(
    string attachmentId, string type) 
    : Specification<Attachment>
{
    public override Expression<Func<Attachment, bool>> ToExpression()
    {
        return attachment => attachment.Id == attachmentId && attachment.ContentType.Contains(type);
    }
}