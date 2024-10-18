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
    
    [BindNever]
    public Guid ChatId { get; set; }
    
    [BindNever]
    public Guid MemberId { get; set; }
    
    public Guid UserBlogId { get; set; }

}