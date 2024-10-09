using Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("members/{memberId}/roles")]
public class ChatMemberRoleController(
    IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> SetRoleToMember(Guid memberId, [FromBody]SetRoleToMemberCommand setRoleToMemberCommand,
        CancellationToken cancellationToken = default)
    {
        setRoleToMemberCommand.UserId = UserId;
        setRoleToMemberCommand.MemberId = memberId;
        
        var chatMember = await mediator.Send(setRoleToMemberCommand, cancellationToken);
        
        return Ok(chatMember);
    }
}