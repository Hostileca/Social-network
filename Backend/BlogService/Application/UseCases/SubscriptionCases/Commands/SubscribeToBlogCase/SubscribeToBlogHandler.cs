using Application.Dtos;
using Application.Exceptions;
using Application.MappingConfigurations;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;

public class SubscribeToBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<SubscribeToBlogCommand, BlogSubscriptionsReadDto>
{
    public async Task<BlogSubscriptionsReadDto> Handle(SubscribeToBlogCommand request, CancellationToken cancellationToken)
    {
        var currentBlog = await blogRepository.GetByIdAsync(request.UserBlogId, cancellationToken);

        if (currentBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (currentBlog.UserId != request.UserId)
        {
            throw new UnauthorizedException("It is not your blog");
        }
        
        var blogToSubscribe = await blogRepository.GetByIdAsync(request.SubscribeAtId, cancellationToken);

        if (blogToSubscribe is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        if (currentBlog.Subscriptions
            .Any(x => x.SubscribedAtId == request.SubscribeAtId))
        {
            throw new AlreadyExistException(typeof(Subscription).ToString());
        }
        
        var newSubscriber = request.Adapt<Subscription>();
        
        await subscriberRepository.AddAsync(newSubscriber, cancellationToken);

        await subscriberRepository.SaveChangesAsync(cancellationToken);
        
        return currentBlog.Adapt<BlogSubscriptionsReadDto>();
    }
}