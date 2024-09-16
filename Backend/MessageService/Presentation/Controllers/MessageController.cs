using Application.UseCases.MessageCases.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chat/{chatId}/messages")]
public class MessageController(
    IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendMessage(Guid chatId,
        SendMessageCommand sendMessageCommand)
    {
        sendMessageCommand.UserId = UserId;
        sendMessageCommand.ChatId = chatId;
        
        var message = await mediator.Send(sendMessageCommand);

        return Ok(message);
    }
}