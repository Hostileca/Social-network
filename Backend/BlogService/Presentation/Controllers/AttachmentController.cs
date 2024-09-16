using Application.UseCases.AttachmentCases.Queries.LoadAttachmentByIdCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("attachments")]  
public class AttachmentController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpGet("{attachmentId}")]
    public async Task<IActionResult> GetAttachmentById(string attachmentId, CancellationToken cancellationToken)
    {
        var attachment = await mediator.Send(new LoadAttachmentByIdQuery
        {
            Id = attachmentId
        }, cancellationToken);
        
        return File(attachment.File, attachment.ContentType);
    }
}