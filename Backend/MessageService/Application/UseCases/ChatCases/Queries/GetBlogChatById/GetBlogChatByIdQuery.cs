using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChatById;

public class GetBlogChatByIdQuery : IRequest<ChatReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
}