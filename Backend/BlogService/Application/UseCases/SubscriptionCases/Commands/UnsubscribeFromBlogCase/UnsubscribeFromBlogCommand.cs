using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<SubscriptionReadDto>
{
    [FromRoute]
    public string? UserBlogId { get; set; }
    
    [BindNever]
    public string? UserId { get; set; }
    
    [FromRoute]
    public string SubscriptionId { get; set; }
}