using Application.UseCases.LikeCases.Commands.AddLikeToPostCase;
using Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("posts/{postId}/likes")] 
public class LikeController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddLike(string postId, AddLikeToPostCommand addLikeToPostCommand,
        CancellationToken cancellationToken)
    {
        addLikeToPostCommand.PostId = postId;
        addLikeToPostCommand.UserId = UserId;
        
        var comment = await mediator.Send(addLikeToPostCommand, cancellationToken);
        
        return Ok(comment);
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveLike(string postId, RemoveLikeFromPostCommand removeLikeFromPostCommand,
        CancellationToken cancellationToken)
    {
        removeLikeFromPostCommand.PostId = postId;
        removeLikeFromPostCommand.UserId = UserId;
        
        var comment = await mediator.Send(removeLikeFromPostCommand, cancellationToken);
        
        return Ok(comment);
    }
}