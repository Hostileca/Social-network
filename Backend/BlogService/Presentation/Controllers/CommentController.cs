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
    public async Task<IActionResult> GetPostComments(string postId, CancellationToken cancellationToken)
    {
        var comments = await mediator.Send(new GetPostCommentsQuery
        {
            PostId = postId
        }, cancellationToken);
        
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment(string postId, CreateCommentCommand createCommentCommand,
        CancellationToken cancellationToken)
    {
        createCommentCommand.PostId = postId;
        createCommentCommand.UserId = UserId;
        
        var comment = await mediator.Send(createCommentCommand, cancellationToken);
        
        return Ok(comment);
    }
}