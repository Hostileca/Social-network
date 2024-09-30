using SharedResources.Dtos;

namespace Application.SignalR.Services;

public interface IChatNotificationService
{
    Task CreateChatAsync(ChatReadDto chatReadDto, CancellationToken cancellationToken);
    Task DeleteChatAsync(ChatReadDto chatReadDto, CancellationToken cancellationToken);
    Task JoinChatsAsync(string connectionId, IEnumerable<ChatReadDto> chats, CancellationToken cancellationToken);
}