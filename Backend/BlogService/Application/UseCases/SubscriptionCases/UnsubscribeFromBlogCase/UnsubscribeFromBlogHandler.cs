using Application.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.SubscriptionCases.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogHandler(
    ISubscriberRepository subscriberRepository) 
    : IRequestHandler<UnsubscribeFromBlogCommand, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(UnsubscribeFromBlogCommand request, CancellationToken cancellationToken)
    {
        throw new Exception();
    }
    
}