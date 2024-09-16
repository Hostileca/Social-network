using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ReactionCases.RemoveReaction;

public class RemoveReactionCommand : IRequest<ReactionReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid MessageId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public Guid ReactionId { get; set; }
}