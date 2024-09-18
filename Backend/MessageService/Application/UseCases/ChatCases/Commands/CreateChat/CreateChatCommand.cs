using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public string Name { get; set; }
    
    public List<Guid> OtherMembers { get; set; }
}