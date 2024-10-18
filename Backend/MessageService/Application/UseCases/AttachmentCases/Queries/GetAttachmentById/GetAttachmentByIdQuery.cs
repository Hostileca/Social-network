using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.AttachmentCases.Queries.GetAttachmentById;

public class GetAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [BindNever]
    public Guid ChatId { get; set; }
    
    [BindNever]
    public Guid MessageId { get; set; }
    
    [BindNever]
    public Guid AttachmentId { get; set; }
    
    public Guid UserBlogId { get; set; }
}