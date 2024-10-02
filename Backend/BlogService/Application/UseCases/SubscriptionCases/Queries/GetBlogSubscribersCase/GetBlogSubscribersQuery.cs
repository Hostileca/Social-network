using System.Text.Json.Serialization;
using MediatR;
using SharedResources;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersQuery : IRequest<IEnumerable<SubscriberReadDto>>
{
    [JsonIgnore]
    public string? BlogId { get; set; }
}