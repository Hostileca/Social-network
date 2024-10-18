using SharedResources.Dtos;

namespace Application.SignalR.Services;

public interface IChatNotificationService
{
    Task CreateChatAsync(ChatReadDto chatReadDto, IEnumerable<ChatMemberReadDto> chatMembers, CancellationToken cancellationToken);
    Task DeleteChatAsync(ChatReadDto chatReadDto, IEnumerable<ChatMemberReadDto> chatMembers,CancellationToken cancellationToken);
    Task JoinChatsAsync(string connectionId, IEnumerable<ChatReadDto> chats, CancellationToken cancellationToken);
}