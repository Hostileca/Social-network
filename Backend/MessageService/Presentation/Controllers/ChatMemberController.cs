using Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;
using Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats/{chatId}/members")]
public class ChatMemberController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddMemberToChat(Guid chatId, 
        AddMemberToChatCommand addMemberToChatCommand,
        CancellationToken cancellationToken = default)
    {
        addMemberToChatCommand.UserId = UserId;
        addMemberToChatCommand.ChatId = chatId;

        var chatMember = await mediator.Send(addMemberToChatCommand, cancellationToken);

        return Ok(chatMember);
    }
    
    [HttpDelete("{memberId}")]
    public async Task<IActionResult> DeleteMemberFromChat(Guid chatId, Guid memberId,
        RemoveMemberFromChatCommand removeMemberFromChatCommand,
        CancellationToken cancellationToken = default)
    {
        removeMemberFromChatCommand.UserId = UserId;
        removeMemberFromChatCommand.ChatId = chatId;
        removeMemberFromChatCommand.ChatMemberId = memberId;

        var chatMember = await mediator.Send(removeMemberFromChatCommand, cancellationToken);

        return Ok(chatMember);
    }
}