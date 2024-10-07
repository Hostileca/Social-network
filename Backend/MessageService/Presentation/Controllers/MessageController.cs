using Application.UseCases.MessageCases.Commands.SendDelayedMessage;
using Application.UseCases.MessageCases.Commands.SendMessage;
using Application.UseCases.MessageCases.Queries.GetChatMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats/{chatId}")]
public class MessageController(
    IMediator mediator)
    : ControllerBase
{
    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages(GetChatMessagesQuery getChatMessagesQuery,
        CancellationToken cancellationToken = default)
    {
        getChatMessagesQuery.UserId = UserId;
        
        var messages = await mediator.Send(getChatMessagesQuery, cancellationToken);
        
        return Ok(messages);
    }
    
    [HttpPost("messages")]
    public async Task<IActionResult> SendMessage(SendMessageCommand sendMessageCommand, 
    CancellationToken cancellationToken = default)
    {
        sendMessageCommand.UserId = UserId;
        
        var message = await mediator.Send(sendMessageCommand, cancellationToken);

        return Ok(message);
    }

    [HttpPost("delayed-messages")]
    public async Task<IActionResult> SendDelayedMessage(SendDelayedMessageCommand sendDelayedMessageCommand, 
        CancellationToken cancellationToken = default)
    {
        sendDelayedMessageCommand.UserId = UserId;
        
        var message = await mediator.Send(sendDelayedMessageCommand, cancellationToken);

        return Ok(message);
    }
}