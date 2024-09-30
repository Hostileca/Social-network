using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
    
public class SubscribeToBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    [JsonIgnore]
    public string? UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; } 
    public string SubscribeAtId { get; set; }
}