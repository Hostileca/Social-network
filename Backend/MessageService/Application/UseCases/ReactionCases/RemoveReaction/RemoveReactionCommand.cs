using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ReactionCases.RemoveReaction;

public class RemoveReactionCommand : IRequest<ReactionReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromRoute]
    public Guid MessageId { get; set; }
        
    [FromRoute]
    public Guid ReactionId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
}