using Application.UseCases.AttachmentCases.Queries.GetAttachmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats/{chatId}/messages/{messageId}/attachments")]
public class AttachmentController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpGet("{attachmentId}")]
    public async Task<IActionResult> GetMessageAttachment([FromQuery]GetAttachmentByIdQuery getAttachmentByIdQuery,
        CancellationToken cancellationToken = default)
    {
        getAttachmentByIdQuery.UserId = UserId;

        var attachment = await mediator.Send(getAttachmentByIdQuery, cancellationToken);
        
        return File(attachment.File, attachment.ContentType, attachment.FileName);
    }
}