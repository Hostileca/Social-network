using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsQuery : IRequest<IEnumerable<BlogReadDto>>
{
    public string? BlogId { get; set; }
}