using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;

public class SubscribeToBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<SubscribeToBlogCommand, BlogSubscriptionsReadDto>
{
    public async Task<BlogSubscriptionsReadDto> Handle(SubscribeToBlogCommand request, CancellationToken cancellationToken)
    {
        var currentBlog = await blogRepository.GetByIdAndUserIdAsync(request.UserBlogId, 
            request.UserId, cancellationToken);

        if (currentBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId);
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
        
        return currentBlog.Adapt<BlogSubscriptionsReadDto>();
    }
}