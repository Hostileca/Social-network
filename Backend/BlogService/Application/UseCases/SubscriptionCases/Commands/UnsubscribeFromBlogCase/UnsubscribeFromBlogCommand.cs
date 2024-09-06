using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    [JsonIgnore]
    public string? SubscripeAtId { get; set; }
}