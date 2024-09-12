using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid ChatMemberId { get; set; }
}