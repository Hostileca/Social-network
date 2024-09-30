using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.MessageCases.Commands.SendMessage;

public class SendMessageHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IMessageNotificationService messageNotificationService)
    : SendMessageHandlerBase(blogRepository, chatRepository, messageRepository, messageNotificationService),
        IRequestHandler<SendMessageCommand, MessageReadDto>
{
    public async Task<MessageReadDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await CreateMessageAsync(request, cancellationToken);

        var messageReadDto = await SendMessageAsync(message, cancellationToken);
        
        return messageReadDto;
    }
}