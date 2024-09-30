using Application.SignalR.Services;
using Application.UseCases.MessageCases.Commands.SendMessage;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.MessageCases.Commands;

public abstract class SendMessageHandlerBase(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IMessageNotificationService messageNotificationService)
{
    protected async Task<Message> CreateMessageAsync(SendMessageCommandBase request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        if (chat.Members.All(m => m.BlogId != userBlog.Id))
        {
            throw new NoPermissionException("You are not a member of this chat");
        }

        var message = request.Adapt<Message>();
        
        return message;
    }

    public async Task<MessageReadDto> SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        await messageRepository.AddAsync(message, cancellationToken);
        
        await messageRepository.SaveChangesAsync(cancellationToken);

        var messageReadDto = message.Adapt<MessageReadDto>();
        
        await messageNotificationService.SendMessageAsync(messageReadDto, message.ChatId, cancellationToken);

        return messageReadDto;
    }
}