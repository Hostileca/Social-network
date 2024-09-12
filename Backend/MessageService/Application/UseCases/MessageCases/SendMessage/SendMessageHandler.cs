using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.MessageCases.SendMessage;

public class SendMessageHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IMessageRepository messageRepository)
    : IRequestHandler<SendMessageCommand, MessageReadDto>
{
    public async Task<MessageReadDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetByIdAsync(request.UserBlogId, cancellationToken);

        if (userBlog is null || userBlog.UserId != request.UserId)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        var message = request.Adapt<Message>();

        await messageRepository.AddAsync(message, cancellationToken);
        
        await messageRepository.SaveChangesAsync(cancellationToken);
        
        return message.Adapt<MessageReadDto>();
    }
}