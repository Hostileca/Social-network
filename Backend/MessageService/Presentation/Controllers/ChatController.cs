using Application.UseCases.ChatCases.Commands.CreateChat;
using Application.UseCases.ChatCases.Commands.DeleteChat;
using Application.UseCases.ChatCases.Queries.GetBlogChatById;
using Application.UseCases.ChatCases.Queries.GetBlogChats;
using Application.UseCases.ChatMembersCases.Commands.LeaveChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats")]
public class ChatController(
    IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetChats([FromQuery]Guid userBlogId, CancellationToken cancellationToken = default)
    {
        var chats = await mediator.Send(new GetBlogChatsQuery
        {
            UserId = UserId,
            UserBlogId = userBlogId
        }, cancellationToken);

        return Ok(chats);
    }
    
    [HttpGet("chatId")]
    public async Task<IActionResult> GetChatById(Guid chatId, [FromQuery]Guid userBlogId, 
        CancellationToken cancellationToken = default)
    {
        var chats = await mediator.Send(new GetBlogChatByIdQuery
        {
            UserId = UserId,
            ChatId = chatId,
            UserBlogId = userBlogId
        }, cancellationToken);

        return Ok(chats);
    }

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
    
    [HttpDelete("{chatId}/leave")]
    public async Task<IActionResult> LeaveChat(Guid chatId, LeaveChatCommand leaveChatCommand,
        CancellationToken cancellationToken = default)
    {
        leaveChatCommand.ChatId = chatId;
        leaveChatCommand.UserId = UserId;

        var chat = await mediator.Send(leaveChatCommand, cancellationToken);

        return Ok(chat);
    }
}