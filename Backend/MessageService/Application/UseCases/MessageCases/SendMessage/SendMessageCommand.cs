using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.MessageCases.SendMessage;

public class SendMessageCommand : IRequest<MessageReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public Guid ChatId { get; set; }
    
    public string Text { get; set; }
}