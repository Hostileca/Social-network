using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
    
public class SubscribeToBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; } 
    [JsonIgnore]
    public string? SubscribeAtId { get; set; }
}