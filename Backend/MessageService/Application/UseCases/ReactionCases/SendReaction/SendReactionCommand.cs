using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionCommand : IRequest<ReactionReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [FromRoute]
    [JsonIgnore]
    public Guid MessageId { get; set; }
    
    [FromQuery]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public string Emoji { get; set; }
}