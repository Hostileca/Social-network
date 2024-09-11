using Application.Dtos;
using Application.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogChatsQuery, BlogChatsReadDto>
{
    public async Task<BlogChatsReadDto> Handle(GetBlogChatsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }

        return blog.Adapt<BlogChatsReadDto>();
    }
}