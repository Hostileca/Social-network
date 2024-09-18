using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetBlogSubscribersQuery, BlogSubscribersReadDto>
{
    public async Task<BlogSubscribersReadDto> Handle(GetBlogSubscribersQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        return blog.Adapt<BlogSubscribersReadDto>();
    }
}