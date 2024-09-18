using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogChatsQuery, BlogChatsReadDto>
{
    public async Task<BlogChatsReadDto> Handle(GetBlogChatsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        return blog.Adapt<BlogChatsReadDto>();
    }
}