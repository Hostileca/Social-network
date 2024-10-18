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
    public async Task<IActionResult> GetPostComments(string postId, [FromQuery]GetPostCommentsQuery getPostCommentsQuery, 
        CancellationToken cancellationToken = default)
    {
        getPostCommentsQuery.PostId = postId;
        
        var comments = await mediator.Send(getPostCommentsQuery, cancellationToken);
        
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment(string postId, [FromQuery]string UserBlogId, [FromBody]CreateCommentCommand createCommentCommand,
        CancellationToken cancellationToken)
    {
        createCommentCommand.UserId = UserId;
        createCommentCommand.PostId = postId;
        createCommentCommand.UserBlogId = UserBlogId;
        
        var comment = await mediator.Send(createCommentCommand, cancellationToken);
        
        return Ok(comment);
    }
}