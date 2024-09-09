using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogSubscriptionsQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetBlogSubscriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        return blog.Subscribtions.Adapt<IEnumerable<BlogReadDto>>();
    }
}