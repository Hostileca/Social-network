using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogChatsQuery, IEnumerable<ChatReadDto>>
{
    public async Task<IEnumerable<ChatReadDto>> Handle(GetBlogChatsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        return blog.ChatsMember.Adapt<IEnumerable<ChatReadDto>>();
    }
}