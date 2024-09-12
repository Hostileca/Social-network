using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;

public class AddMemberToChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    [JsonIgnore]
    public Guid ChatId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public Guid BlogToAddId { get; set; } 
}