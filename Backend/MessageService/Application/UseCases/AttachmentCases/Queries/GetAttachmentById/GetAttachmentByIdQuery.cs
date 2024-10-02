using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.AttachmentCases.Queries.GetAttachmentById;

public class GetAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    public string UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public Guid ChatId { get; set; }
    
    public Guid MessageId { get; set; }
    
    public Guid AttachmentId { get;set; }
}