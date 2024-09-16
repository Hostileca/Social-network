using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionCommand : IRequest<ReactionReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public string Emoji { get; set; }
    
    public Guid ChatId { get; set; }
    
    public Guid MessageId { get; set; }
}