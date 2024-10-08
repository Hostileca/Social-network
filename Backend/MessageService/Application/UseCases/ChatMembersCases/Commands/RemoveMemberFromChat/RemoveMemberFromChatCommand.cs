using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommand : IRequest<ChatMemberReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromRoute]
    public Guid ChatId { get; set; }
    
    [FromRoute]
    public Guid MemberId { get; set; }
    
    public Guid UserBlogId { get; set; }

}