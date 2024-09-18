using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.LikeCases.Queries.GetBlogLikesCase;

public class GetBlogLikesHandler(
    IBlogRepository blogRepository) : IRequestHandler<GetBlogLikesQuery, BlogLikesReadDto>
{
    public async Task<BlogLikesReadDto> Handle(GetBlogLikesQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }

        return blog.Adapt<BlogLikesReadDto>();
    }
}