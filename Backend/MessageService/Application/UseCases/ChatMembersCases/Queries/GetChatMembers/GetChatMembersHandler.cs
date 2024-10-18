using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ChatMembersCases.Queries.GetChatMembers;

public class GetChatMembersHandler(
    IBlogRepository blogRepository,
    IChatMemberRepository chatMemberRepository)
    : IRequestHandler<GetChatMembersQuery, IEnumerable<ChatMemberReadDto>>
{
    public async Task<IEnumerable<ChatMemberReadDto>> Handle(GetChatMembersQuery request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        var chatMember = userBlog.ChatsMember.FirstOrDefault(x => x.ChatId == request.ChatId);
        
        if (chatMember is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        var pagedFilter = request.Adapt<PagedFilter>();
        
        var chatMembers = await chatMemberRepository.GetChatMembersByChatIdAsync(pagedFilter, request.ChatId, cancellationToken);
        
        return chatMembers.Adapt<IEnumerable<ChatMemberReadDto>>();
    }
}