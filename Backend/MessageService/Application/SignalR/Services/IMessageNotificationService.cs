using Application.Dtos;

namespace Application.SignalR.Services;

public interface IMessageNotificationService
{ 
    Task SendMessageAsync(MessageReadDto messageReadDto, Guid chatId, CancellationToken cancellationToken);
}