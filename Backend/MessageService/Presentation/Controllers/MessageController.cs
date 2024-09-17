using Application.UseCases.MessageCases.Commands.SendMessage;
using Application.UseCases.MessageCases.Queries.GetChatMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chat/{chatId}/messages")]
public class MessageController(
    IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMessages(Guid chatId, GetChatMessagesQuery getChatMessagesQuery,
        CancellationToken cancellationToken = default)
    {
        getChatMessagesQuery.UserId = UserId;
        getChatMessagesQuery.ChatId = chatId;
        
        var messages = await mediator.Send(getChatMessagesQuery, cancellationToken);
        
        return Ok(messages);
    }
    
    [HttpPost]
    public async Task<IActionResult> SendMessage(Guid chatId,
        SendMessageCommand sendMessageCommand, CancellationToken cancellationToken = default)
    {
        sendMessageCommand.UserId = UserId;
        sendMessageCommand.ChatId = chatId;
        
        var message = await mediator.Send(sendMessageCommand, cancellationToken);

        return Ok(message);
    }
}