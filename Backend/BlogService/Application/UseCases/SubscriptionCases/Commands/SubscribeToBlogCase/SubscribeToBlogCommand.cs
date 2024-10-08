using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
    
public class SubscribeToBlogCommand : IRequest<SubscriptionReadDto>
{
    [FromQuery]
    [JsonIgnore]
    public string? UserBlogId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; } 
    
    [JsonIgnore]
    public string SubscribeAtId { get; set; }
}