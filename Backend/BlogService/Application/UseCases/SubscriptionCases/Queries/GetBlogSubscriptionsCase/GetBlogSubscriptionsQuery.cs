using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsQuery : IRequest<BlogSubscriptionsReadDto>
{
    public string? BlogId { get; set; }
}