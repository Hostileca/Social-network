using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogChatsQuery, IEnumerable<ChatReadDto>>
{
    public async Task<IEnumerable<ChatReadDto>> Handle(GetBlogChatsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        return blog.ChatsMember.Adapt<IEnumerable<ChatReadDto>>();
    }
}