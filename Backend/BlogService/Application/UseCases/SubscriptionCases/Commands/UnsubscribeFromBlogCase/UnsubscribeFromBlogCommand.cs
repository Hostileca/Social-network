using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<SubscriptionReadDto>
{
    [JsonIgnore]
    public string? UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string Id { get; set; }
}