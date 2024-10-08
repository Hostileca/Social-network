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
    public async Task<IActionResult> GetChats([FromQuery]GetBlogChatsQuery getBlogChatsQuery,
        CancellationToken cancellationToken = default)
    {
        getBlogChatsQuery.UserId = UserId;
        
        var chats = await mediator.Send(getBlogChatsQuery, cancellationToken);

        return Ok(chats);
    }
    
    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetChatById([FromQuery]GetBlogChatByIdQuery getBlogChatByIdQuery, 
        CancellationToken cancellationToken = default)
    {
        getBlogChatByIdQuery.UserId = UserId;
            
        var chats = await mediator.Send(getBlogChatByIdQuery, cancellationToken);

        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody]CreateChatCommand createChatCommand, 
        CancellationToken cancellationToken = default)
    {
        createChatCommand.UserId = UserId;

        var chat = await mediator.Send(createChatCommand, cancellationToken);

        return Ok(chat);
    }
    
    [HttpDelete("{chatId}")]
    public async Task<IActionResult> DeleteChat([FromQuery]DeleteChatCommand deleteChatCommand,
        CancellationToken cancellationToken = default)
    {
        deleteChatCommand.UserId = UserId;

        var chat = await mediator.Send(deleteChatCommand, cancellationToken);

        return Ok(chat);
    }
    
    [HttpDelete("{chatId}/leave")]
    public async Task<IActionResult> LeaveChat([FromQuery]LeaveChatCommand leaveChatCommand,
        CancellationToken cancellationToken = default)
    {
        leaveChatCommand.UserId = UserId;

        var chat = await mediator.Send(leaveChatCommand, cancellationToken);

        return Ok(chat);
    }
}