using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ChatCases.Queries.GetBlogChatById;

public class GetBlogChatByIdHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogChatByIdQuery, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(GetBlogChatByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chatMember = blog.ChatsMember.FirstOrDefault(cm => cm.ChatId == request.ChatId);
        
        if (chatMember is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }
        
        return chatMember.Chat.Adapt<ChatReadDto>();
    }
}