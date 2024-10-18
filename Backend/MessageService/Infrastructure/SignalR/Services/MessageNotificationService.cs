using Application.SignalR.Services;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using SharedResources.Dtos;

namespace Infrastructure.SignalR.Services;

public class MessageNotificationService(
    IHubContext<ChatHub> chatHub)
    : IMessageNotificationService
{
    public async Task SendMessageAsync(MessageReadDto messageReadDto, Guid chatId, 
        CancellationToken cancellationToken = default)
    {
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.MessageSent, messageReadDto, cancellationToken);
    }
}