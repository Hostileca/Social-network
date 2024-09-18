using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionCommand : IRequest<ReactionReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid MessageId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public string Emoji { get; set; }
}