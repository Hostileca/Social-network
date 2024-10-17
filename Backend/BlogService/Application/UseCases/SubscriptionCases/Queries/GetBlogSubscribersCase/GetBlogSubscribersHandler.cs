using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<GetBlogSubscribersQuery, IEnumerable<SubscriberReadDto>>
{
    public async Task<IEnumerable<SubscriberReadDto>> Handle(GetBlogSubscribersQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var pagedFilter = request.Adapt<PagedFilter>();
        
        var subscribers = await subscriberRepository.GetBlogSubscribers(pagedFilter, 
            request.BlogId, cancellationToken);
        
        return subscribers.Adapt<IEnumerable<SubscriberReadDto>>();
    }
}