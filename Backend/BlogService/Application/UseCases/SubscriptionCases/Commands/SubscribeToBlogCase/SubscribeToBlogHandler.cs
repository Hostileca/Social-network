using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;

public class SubscribeToBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository,
    ICacheRepository cacheRepository) 
    : IRequestHandler<SubscribeToBlogCommand, SubscriptionReadDto>
{
    public async Task<SubscriptionReadDto> Handle(SubscribeToBlogCommand request, CancellationToken cancellationToken)
    {
        var currentBlog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, 
            request.UserId, cancellationToken);

        if (currentBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var blogToSubscribe = await blogRepository.GetByIdAsync(request.SubscribeAtId, cancellationToken);

        if (blogToSubscribe is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.SubscribeAtId);
        }
        
        if (currentBlog.Subscriptions
            .Any(x => x.SubscribedAtId == request.SubscribeAtId))
        {
            throw new AlreadyExistsException(typeof(Subscription).ToString());
        }
        
        var newSubscriber = request.Adapt<Subscription>();
        
        await subscriberRepository.AddAsync(newSubscriber, cancellationToken);

        await subscriberRepository.SaveChangesAsync(cancellationToken);

        var subscriberBlog = newSubscriber.SubscribedBy.Adapt<BlogReadDto>();
        var subscriptionAtBlog = newSubscriber.SubscribedAtId.Adapt<BlogReadDto>();
        
        await cacheRepository.SetAsync(newSubscriber.SubscribedById, subscriberBlog,
            CacheConfig.BlogCacheTime);
        
        await cacheRepository.SetAsync(newSubscriber.SubscribedAtId, subscriptionAtBlog,
            CacheConfig.BlogCacheTime);
        
        return newSubscriber.Adapt<SubscriptionReadDto>();
    }
}