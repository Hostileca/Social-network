using Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;
using Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;
using Application.UseCases.ChatMembersCases.Queries.GetChatMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("chats/{chatId}/members")]
public class ChatMemberController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddMemberToChat(Guid chatId, [FromQuery]Guid userBlogId, [FromBody]AddMemberToChatCommand addMemberToChatCommand,
        CancellationToken cancellationToken = default)
    {
        addMemberToChatCommand.UserId = UserId;
        addMemberToChatCommand.ChatId = chatId;
        addMemberToChatCommand.UserBlogId = userBlogId;

        var chatMember = await mediator.Send(addMemberToChatCommand, cancellationToken);

        return Ok(chatMember);
    }
    
    [HttpDelete("{memberId}")]
    public async Task<IActionResult> DeleteMemberFromChat(Guid chatId, Guid memberId, 
        [FromQuery]RemoveMemberFromChatCommand removeMemberFromChatCommand, CancellationToken cancellationToken = default)
    {
        removeMemberFromChatCommand.UserId = UserId;
        removeMemberFromChatCommand.ChatId = chatId;
        removeMemberFromChatCommand.MemberId = memberId;

        var chatMember = await mediator.Send(removeMemberFromChatCommand, cancellationToken);

        return Ok(chatMember);
    }

    [HttpGet]
    public async Task<IActionResult> GetChatMembers(Guid chatId, [FromQuery]GetChatMembersQuery getChatMembersQuery,
        CancellationToken cancellationToken = default)
    {
        getChatMembersQuery.UserId = UserId;
        getChatMembersQuery.ChatId = chatId;

        var members = await mediator.Send(getChatMembersQuery, cancellationToken);

        return Ok(members);
    }
}