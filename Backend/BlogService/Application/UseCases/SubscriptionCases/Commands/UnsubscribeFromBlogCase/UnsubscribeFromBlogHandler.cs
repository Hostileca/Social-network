using Application.Dtos;
using Application.Exceptions;
using Application.MappingConfigurations;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogHandler(
    IBlogRepository blogRepository,
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<UnsubscribeFromBlogCommand, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(UnsubscribeFromBlogCommand request, CancellationToken cancellationToken)
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
        
        var subscribtion = currentBlog.Subscribtions
            .FirstOrDefault(x => x.SubscribedById == request.SubscripeAtId);

        if (subscribtion is null)
        {
            throw new NotFoundException(typeof(Subscription).ToString());
        }
        
        subscriberRepository.Delete(subscribtion);

        await subscriberRepository.SaveChangesAsync(cancellationToken);
        
        return currentBlog.Subscribtions.Adapt<IEnumerable<BlogReadDto>>();
    }
    
}