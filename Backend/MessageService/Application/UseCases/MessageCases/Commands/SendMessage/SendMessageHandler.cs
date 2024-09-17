using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.MessageCases.Commands.SendMessage;

public class SendMessageHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IMessageNotificationService messageNotificationService)
    : IRequestHandler<SendMessageCommand, MessageReadDto>
{
    public async Task<MessageReadDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
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

        await messageRepository.AddAsync(message, cancellationToken);
        
        await messageRepository.SaveChangesAsync(cancellationToken);

        var messageReadDto = message.Adapt<MessageReadDto>();
        
        await messageNotificationService.SendMessageAsync(messageReadDto, chat.Id, cancellationToken);
        
        return messageReadDto;
    }
}