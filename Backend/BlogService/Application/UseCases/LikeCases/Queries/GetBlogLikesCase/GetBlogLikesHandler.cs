using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.LikeCases.Queries.GetBlogLikesCase;

public class GetBlogLikesHandler(
    IBlogRepository blogRepository) : IRequestHandler<GetBlogLikesQuery, BlogLikesReadDto>
{
    public async Task<BlogLikesReadDto> Handle(GetBlogLikesQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        return blog.Adapt<BlogLikesReadDto>();
    }
}