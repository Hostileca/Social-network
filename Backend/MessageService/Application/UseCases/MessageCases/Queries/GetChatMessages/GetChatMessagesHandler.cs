using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesHandler(
    IMessageRepository messageRepository, 
    IBlogRepository blogRepository)
    : IRequestHandler<GetChatMessagesQuery, IEnumerable<MessageReadDto>>
{
    public async Task<IEnumerable<MessageReadDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
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

        var messages =
            await messageRepository.GetChatMessages(request.PageNumber, request.PageSize, request.ChatId, cancellationToken);
        return messages.Adapt<IEnumerable<MessageReadDto>>();
    }
}