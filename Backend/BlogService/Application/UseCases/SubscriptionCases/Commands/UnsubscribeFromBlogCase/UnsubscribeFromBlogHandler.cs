using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository,
    ICacheRepository cacheRepository) 
    : IRequestHandler<UnsubscribeFromBlogCommand, SubscriptionReadDto>
{
    public async Task<SubscriptionReadDto> Handle(UnsubscribeFromBlogCommand request, CancellationToken cancellationToken)
    {
        var currentBlog = await blogRepository.GetByIdAndUserIdAsync(request.UserBlogId,
            request.UserId, cancellationToken);

        if (currentBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId);
        }
        
        var subscription = currentBlog.Subscriptions
            .FirstOrDefault(x => x.Id == request.SubscriptionId);

        if (subscription is null)
        {
            throw new NotFoundException(typeof(Subscription).ToString(), request.SubscriptionId);
        }
        
        subscriberRepository.Delete(subscription);

        await subscriberRepository.SaveChangesAsync(cancellationToken);

        var subscriberBlog = subscription.SubscribedBy.Adapt<BlogReadDto>();
        var subscriptionAtBlog = subscription.SubscribedAtId.Adapt<BlogReadDto>();
        
        await cacheRepository.SetAsync(subscription.SubscribedById, subscriberBlog,
            CacheConfig.BlogCacheTime);
        
        await cacheRepository.SetAsync(subscription.SubscribedAtId, subscriptionAtBlog,
            CacheConfig.BlogCacheTime);
        
        return subscription.Adapt<SubscriptionReadDto>();
    }
    
}