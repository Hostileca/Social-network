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
    public async Task<IActionResult> AddMemberToChat([FromBody]AddMemberToChatCommand addMemberToChatCommand,
        CancellationToken cancellationToken = default)
    {
        addMemberToChatCommand.UserId = UserId;

        var chatMember = await mediator.Send(addMemberToChatCommand, cancellationToken);

        return Ok(chatMember);
    }
    
    [HttpDelete("{memberId}")]
    public async Task<IActionResult> DeleteMemberFromChat(RemoveMemberFromChatCommand removeMemberFromChatCommand,
        CancellationToken cancellationToken = default)
    {
        removeMemberFromChatCommand.UserId = UserId;

        var chatMember = await mediator.Send(removeMemberFromChatCommand, cancellationToken);

        return Ok(chatMember);
    }
}