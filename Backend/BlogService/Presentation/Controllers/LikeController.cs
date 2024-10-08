using Application.UseCases.LikeCases.Commands.AddLikeToPostCase;
using Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;
using Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;
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
    public async Task<IActionResult> AddLike([FromBody]AddLikeToPostCommand addLikeToPostCommand,
        CancellationToken cancellationToken = default)
    {
        addLikeToPostCommand.UserId = UserId;
        
        var comment = await mediator.Send(addLikeToPostCommand, cancellationToken);
        
        return Ok(comment);
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveLike([FromQuery]RemoveLikeFromPostCommand removeLikeFromPostCommand,
        CancellationToken cancellationToken = default)
    {
        removeLikeFromPostCommand.UserId = UserId;
        
        var comment = await mediator.Send(removeLikeFromPostCommand, cancellationToken);
        
        return Ok(comment);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLikes([FromQuery]GetLikeSendersByPostIdQuery getLikeSendersByPostIdQuery,
        CancellationToken cancellationToken = default)
    {
        var comment = await mediator.Send(getLikeSendersByPostIdQuery, cancellationToken);
        
        return Ok(comment);
    }
}