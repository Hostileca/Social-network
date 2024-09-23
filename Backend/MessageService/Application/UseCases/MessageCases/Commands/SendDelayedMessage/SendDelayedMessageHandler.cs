using Application.SignalR.Services;
using Application.UseCases.MessageCases.Commands.SendMessage;
using Domain.Repositories;
using Hangfire;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Commands.SendDelayedMessage;

public class SendDelayedMessageHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IMessageNotificationService messageNotificationService)
    : SendMessageHandlerBase(blogRepository, chatRepository, messageRepository, messageNotificationService), 
        IRequestHandler<SendDelayedMessageCommand, DelayedMessageReadDto>
{
    public async Task<DelayedMessageReadDto> Handle(SendDelayedMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await CreateMessageAsync(request, cancellationToken);
        
        var jobId = BackgroundJob.Schedule(() => SendMessageAsync(message, cancellationToken), request.Delay);
        var delayedMessage = message.Adapt<DelayedMessageReadDto>();
        delayedMessage.JobId = jobId;
                
        return delayedMessage;
    }
}