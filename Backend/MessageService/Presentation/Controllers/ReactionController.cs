using Application.UseCases.ReactionCases.Commands.RemoveReaction;
using Application.UseCases.ReactionCases.Commands.SendReaction;
using Application.UseCases.ReactionCases.Queries.GetReactionsByMessageId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("messages/{messageId}/reactions")]
public class ReactionController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReactions(Guid messageId, GetReactionsByMessageIdQuery getReactionsByMessageIdQuery, 
        CancellationToken cancellationToken = default)
    {
        getReactionsByMessageIdQuery.UserId = UserId;
        getReactionsByMessageIdQuery.MessageId = messageId;
        
        var reactions = await mediator.Send(getReactionsByMessageIdQuery, cancellationToken);

        return Ok(reactions);
    }

    [HttpPost]
    public async Task<IActionResult> SendReaction(Guid messageId, [FromBody]SendReactionCommand sendReactionCommand,
        CancellationToken cancellationToken = default)
    {
        sendReactionCommand.UserId = UserId;
        sendReactionCommand.MessageId = messageId;

        var chatMember = await mediator.Send(sendReactionCommand, cancellationToken);

        return Ok(chatMember);
    }
    
    [HttpDelete("{reactionId}")]
    public async Task<IActionResult> RemoveReaction(Guid messageId, Guid reactionId, [FromQuery]RemoveReactionCommand removeReactionCommand,
        CancellationToken cancellationToken = default)
    {
        removeReactionCommand.UserId = UserId;
        removeReactionCommand.MessageId = messageId;
        removeReactionCommand.ReactionId = reactionId;

        var chatMember = await mediator.Send(removeReactionCommand, cancellationToken);

        return Ok(chatMember);
    }
}