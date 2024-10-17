using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ReactionCases.Queries.GetReactionsByMessageId;

public class GetReactionsByMessageIdQuery : IRequest<IEnumerable<ReactionReadDto>>
{
    [BindNever]
    public string? UserId { get; set; } 
    
    [BindNever]
    public Guid MessageId { get; set; }
    
    public Guid UserBlogId { get; set; }
}