using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository)
    : IRequestHandler<GetBlogChatsQuery, IEnumerable<ChatReadDto>>
{
    public async Task<IEnumerable<ChatReadDto>> Handle(GetBlogChatsQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        var pagedFilter = request.Adapt<PagedFilter>();
        
        var chats = await chatRepository.GetChatsByBlogId(pagedFilter, request.UserBlogId, cancellationToken);
        
        return chats.Adapt<IEnumerable<ChatReadDto>>();
    }
}