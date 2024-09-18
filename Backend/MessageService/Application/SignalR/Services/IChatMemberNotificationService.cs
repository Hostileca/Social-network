using Application.Dtos;

namespace Application.SignalR.Services;

public interface IChatMemberNotificationService
{
    Task AddMemberToChatAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken);

    Task RemoveMemberFromChatAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken);
    
    Task ChatMemberUpdateAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken);
    
    Task ChatMemberLeaveAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken);
}