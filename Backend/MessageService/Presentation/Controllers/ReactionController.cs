using Application.UseCases.ReactionCases.RemoveReaction;
using Application.UseCases.ReactionCases.SendReaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("messages/{messageId}/reactions")]
public class ReactionController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendReaction([FromBody]SendReactionCommand sendReactionCommand,
        CancellationToken cancellationToken = default)
    {
        sendReactionCommand.UserId = UserId;

        var chatMember = await mediator.Send(sendReactionCommand, cancellationToken);

        return Ok(chatMember);
    }
    
    [HttpDelete("{reactionId}")]
    public async Task<IActionResult> RemoveReaction([FromQuery]RemoveReactionCommand removeReactionCommand,
        CancellationToken cancellationToken = default)
    {
        removeReactionCommand.UserId = UserId;

        var chatMember = await mediator.Send(removeReactionCommand, cancellationToken);

        return Ok(chatMember);
    }
}