using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetBlogSubscribersQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetBlogSubscribersQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        return blog.Subscribers.Adapt<IEnumerable<BlogReadDto>>();
    }
}