using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Commands.DeleteChat;

public class DeleteChatCommand : IRequest<ChatReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }

    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}