using Application.Dtos;
using Application.SignalR.Services;
using Domain.Repositories;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Services;

public class ChatMemberNotificationService(
    IHubContext<ChatHub> chatHub,
    IBlogConnectionRepository blogConnectionRepository)
    : IChatMemberNotificationService
{
    public async Task AddMemberToChatAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken)
    {
        var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(
            chatMemberReadDto.Blog.Id, cancellationToken);
            
        foreach (var blogConnection in blogConnections)
        {
            await chatHub.Groups.AddToGroupAsync(blogConnection.ConnectionId, 
                $"chat_{chatId}", cancellationToken);
        }
        
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.ChatMemberAdded, chatMemberReadDto, cancellationToken);
    }

    public async Task RemoveMemberFromChatAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId,
        CancellationToken cancellationToken)
    {
        var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(
            chatMemberReadDto.Blog.Id, cancellationToken);
        
        foreach (var blogConnection in blogConnections)
        {
            await chatHub.Groups.RemoveFromGroupAsync(blogConnection.ConnectionId, 
                $"chat_{chatId}", cancellationToken);
        }
        
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.ChatMemberRemoved, chatMemberReadDto, cancellationToken);
    }

    public async Task ChatMemberUpdateAsync(ChatMemberReadDto chatMemberReadDto, Guid chatId, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"chat_{chatId}").SendAsync(
            ClientEvents.ChatMemberUpdated, chatMemberReadDto, cancellationToken);
    }
}