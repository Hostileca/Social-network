using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;

public class AddMemberToChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public Guid BlogToAddId { get; set; } 
}