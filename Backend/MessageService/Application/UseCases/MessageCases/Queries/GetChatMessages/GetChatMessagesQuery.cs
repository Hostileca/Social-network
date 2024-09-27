using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<IEnumerable<MessageReadDto>>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}