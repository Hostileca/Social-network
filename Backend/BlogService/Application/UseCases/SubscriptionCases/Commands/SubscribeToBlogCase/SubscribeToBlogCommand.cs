using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
    
public class SubscribeToBlogCommand : IRequest<SubscriptionReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; } 
    
    [FromRoute]
    public string? BlogId { get; set; }
    
    public string SubscribeAtId { get; set; }
}