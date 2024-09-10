using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersQuery : IRequest<BlogSubscribersReadDto>
{
    [JsonIgnore]
    public string? BlogId { get; set; }
}