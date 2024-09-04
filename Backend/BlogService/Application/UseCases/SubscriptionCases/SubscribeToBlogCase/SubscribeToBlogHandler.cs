using Application.Dtos;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.SubscribeToBlogCase;

public class SubscribeToBlogHandler(
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<SubscribeToBlogCommand, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(SubscribeToBlogCommand request, CancellationToken cancellationToken)
    {
        var newSubscriber = request.Adapt<Subscriber>();
        
        await subscriberRepository.AddAsync(newSubscriber, cancellationToken);

        await subscriberRepository.SaveChangesAsync(cancellationToken);
        
        return newSubscriber.Blog.Subscribtions.Adapt<IEnumerable<BlogReadDto>>();
    }
}