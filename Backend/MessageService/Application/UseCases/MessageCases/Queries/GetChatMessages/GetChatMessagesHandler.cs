using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetChatMessagesQuery, ChatMessagesReadDto>
{
    public async Task<ChatMessagesReadDto> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId,
            request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        var chatMember = userBlog.ChatsMember.FirstOrDefault(cm => cm.ChatId == request.ChatId);
        
        if (chatMember is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        return chatMember.Chat.Adapt<ChatMessagesReadDto>();
    }
}