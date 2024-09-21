using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsCase;

public class GetBlogPostsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogPostsQuery, IEnumerable<PostReadDto>>
{
    public async Task<IEnumerable<PostReadDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }

        return blog.Posts.Adapt<IEnumerable<PostReadDto>>();
    }
}