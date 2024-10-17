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
    public async Task<IActionResult> GetMessageAttachment(Guid chatId, Guid messageId, Guid attachmentId, [FromQuery]GetAttachmentByIdQuery getAttachmentByIdQuery,
        CancellationToken cancellationToken = default)
    {
        getAttachmentByIdQuery.UserId = UserId;
        getAttachmentByIdQuery.ChatId = chatId;
        getAttachmentByIdQuery.MessageId = messageId;
        getAttachmentByIdQuery.AttachmentId = attachmentId;

        var attachment = await mediator.Send(getAttachmentByIdQuery, cancellationToken);
        
        return File(attachment.File, attachment.ContentType, attachment.FileName);
    }
}