using Application.Dtos;

namespace Application.SignalR.Services;

public interface IReactionNotificationService
{
    Task SendReaction(ReactionReadDto reactionReadDto, Guid chatId, CancellationToken cancellationToken);
    Task RemoveReaction(ReactionReadDto reactionReadDto, Guid chatId, CancellationToken cancellationToken);
}