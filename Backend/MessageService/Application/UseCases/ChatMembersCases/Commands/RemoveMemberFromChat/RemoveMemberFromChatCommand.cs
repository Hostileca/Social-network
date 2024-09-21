using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    [JsonIgnore]
    public Guid ChatMemberId { get; set; }
    
    public Guid UserBlogId { get; set; }

}