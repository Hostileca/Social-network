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
    public async Task<IActionResult> GetMessageAttachment(Guid chatId, Guid messageId, Guid attachmentId, [FromQuery]Guid userBlogId,
        CancellationToken cancellationToken)
    {
        var getAttachmentByIdQuery = new GetAttachmentByIdQuery
        {
            UserBlogId = userBlogId,
            UserId = UserId,
            ChatId = chatId,
            MessageId = messageId,
            AttachmentId = attachmentId
        };

        var attachment = await mediator.Send(getAttachmentByIdQuery, cancellationToken);
        
        return Ok(attachment);
    }
}