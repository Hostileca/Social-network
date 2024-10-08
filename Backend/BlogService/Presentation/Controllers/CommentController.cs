using Application.UseCases.CommentCases.Commands.CreateCommentCase;
using Application.UseCases.CommentCases.Queries.GetPostCommentsCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("posts/{postId}/comments")] 
public class CommentController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPostComments([FromQuery]GetPostCommentsQuery getPostCommentsQuery, 
        CancellationToken cancellationToken = default)
    {
        var comments = await mediator.Send(getPostCommentsQuery, cancellationToken);
        
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody]CreateCommentCommand createCommentCommand,
        CancellationToken cancellationToken)
    {
        createCommentCommand.UserId = UserId;
        
        var comment = await mediator.Send(createCommentCommand, cancellationToken);
        
        return Ok(comment);
    }
}