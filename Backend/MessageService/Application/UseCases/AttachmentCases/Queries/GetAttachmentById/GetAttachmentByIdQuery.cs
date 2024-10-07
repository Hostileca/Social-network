using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.AttachmentCases.Queries.GetAttachmentById;

public class GetAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    [BindNever]
    public string UserId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
    
    [FromRoute]
    public Guid ChatId { get; set; }
    
    [FromRoute]
    public Guid MessageId { get; set; }
    
    [FromRoute]
    public Guid AttachmentId { get; set; }
}