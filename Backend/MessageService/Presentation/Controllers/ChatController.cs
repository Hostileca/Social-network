using Application.UseCases.ChatCases.Commands.CreateChat;
using Application.UseCases.ChatCases.Commands.DeleteChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats")]
public class ChatController(
    IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateChat(CreateChatCommand createChatCommand, 
        CancellationToken cancellationToken = default)
    {
        createChatCommand.UserId = UserId;

        var chat = await mediator.Send(createChatCommand, cancellationToken);

        return Ok(chat);
    }
    
    [HttpDelete("{chatId}")]
    public async Task<IActionResult> DeleteChat(Guid chatId, DeleteChatCommand deleteChatCommand,
        CancellationToken cancellationToken = default)
    {
        deleteChatCommand.ChatId = chatId;
        deleteChatCommand.UserId = UserId;

        var chat = await mediator.Send(deleteChatCommand, cancellationToken);

        return Ok(chat);
    }
}