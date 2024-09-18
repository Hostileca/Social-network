using MediatR;
using Newtonsoft.Json;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<ChatMessagesReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}