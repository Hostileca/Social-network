using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<BlogSubscriptionsReadDto>
{
    public string UserBlogId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    [JsonIgnore]
    public string? SubscripeAtId { get; set; }
}