using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogSubscriptionsQuery, IEnumerable<SubscriptionReadDto>>
{
    public async Task<IEnumerable<SubscriptionReadDto>> Handle(GetBlogSubscriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        return blog.Subscriptions.Adapt<IEnumerable<SubscriptionReadDto>>();
    }
}