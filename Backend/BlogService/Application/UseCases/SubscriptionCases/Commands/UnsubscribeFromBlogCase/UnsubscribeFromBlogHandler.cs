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
        
        var subscribtion = currentBlog.Subscriptions
            .FirstOrDefault(x => x.Id == request.SubscriptionId);

        if (subscribtion is null)
        {
            throw new NotFoundException(typeof(Subscription).ToString(), request.SubscriptionId);
        }
        
        subscriberRepository.Delete(subscribtion);

        await subscriberRepository.SaveChangesAsync(cancellationToken);

        await cacheRepository.SetAsync(subscribtion.SubscribedById, subscribtion.SubscribedBy,
            CacheConfig.BlogCacheTime);
        
        await cacheRepository.SetAsync(subscribtion.SubscribedAtId, subscribtion.SubscribedAt,
            CacheConfig.BlogCacheTime);
        
        return subscribtion.Adapt<SubscriptionReadDto>();
    }
    
}