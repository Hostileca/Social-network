using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    [JsonIgnore]
    public string? UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string UnSubscribeFromId { get; set; }
}