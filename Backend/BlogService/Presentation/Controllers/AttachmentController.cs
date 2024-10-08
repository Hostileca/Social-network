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
    public async Task<IActionResult> GetAttachmentById([FromQuery]LoadAttachmentByIdQuery loadAttachmentByIdQuery, 
        CancellationToken cancellationToken = default)
    {
        var attachment = await mediator.Send(loadAttachmentByIdQuery, cancellationToken);
        
        return File(attachment.File, attachment.ContentType, attachment.FileName);
    }
}