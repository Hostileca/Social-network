using Application.Dtos;
using Application.SignalR.Services;
using Domain.Repositories;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Services;

public class MessageNotificationService(
    IHubContext<ChatHub> chatHub)
    : IMessageNotificationService
{
    public async Task SendMessageAsync(MessageReadDto messageReadDto, Guid chatId, 
        CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.MessageSent, messageReadDto, cancellationToken);
    }
}