using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.LeaveChat;

public class LeaveChatCommand : IRequest<ChatMemberReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromRoute]
    public Guid ChatId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
}