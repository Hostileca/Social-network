using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    [JsonIgnore]
    public Guid BlogId { get; set; }
    
    [JsonIgnore]
    public string UserId { get; set; }
    
    public string Name { get; set; }
    
    public List<Guid> OtherMembers { get; set; }
}