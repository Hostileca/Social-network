using Application.Dtos;
using Application.SignalR.Services;
using Domain.Repositories;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Services;

public class ReactionNotificationService(
    IHubContext<ChatHub> chatHub) 
    : IReactionNotificationService
{
    public async Task SendReaction(ReactionReadDto reactionReadDto, Guid chatId, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.ReactionSent, reactionReadDto, cancellationToken);
    }

    public async Task RemoveReaction(ReactionReadDto reactionReadDto, Guid chatId, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.ReactionRemoved, reactionReadDto, cancellationToken);
    }
}