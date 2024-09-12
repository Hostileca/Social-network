using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
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

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId.ToString());
        }
        
        if (blog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }

        return blog.Adapt<BlogChatsReadDto>();
    }
}