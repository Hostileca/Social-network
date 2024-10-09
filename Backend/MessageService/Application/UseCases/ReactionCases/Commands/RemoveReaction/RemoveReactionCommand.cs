using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ReactionCases.Commands.RemoveReaction;

public class RemoveReactionCommand : IRequest<ReactionReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [BindNever]
    public Guid MessageId { get; set; }
        
    [BindNever]
    public Guid ReactionId { get; set; }
    
    public Guid UserBlogId { get; set; }
}