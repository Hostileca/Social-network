﻿using Application.Dtos;
using Application.Exceptions;
using Application.MappingConfigurations;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<UnsubscribeFromBlogCommand, BlogSubscriptionsReadDto>
{
    public async Task<BlogSubscriptionsReadDto> Handle(UnsubscribeFromBlogCommand request, CancellationToken cancellationToken)
    {
        var currentBlog = await blogRepository.GetByIdAsync(request.UserBlogId, cancellationToken);

        if (currentBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (currentBlog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }
        
        var subscribtion = currentBlog.Subscriptions
            .FirstOrDefault(x => x.SubscribedById == request.UnSubscribeFromId);

        if (subscribtion is null)
        {
            throw new NotFoundException(typeof(Subscription).ToString());
        }
        
        subscriberRepository.Delete(subscribtion);

        await subscriberRepository.SaveChangesAsync(cancellationToken);
        
        return currentBlog.Adapt<BlogSubscriptionsReadDto>();
    }
    
}