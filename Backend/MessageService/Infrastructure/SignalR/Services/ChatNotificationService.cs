using Application.SignalR.Services;
using Domain.Repositories;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using SharedResources.Dtos;

namespace Infrastructure.SignalR.Services;

public class ChatNotificationService(
    IHubContext<ChatHub> chatHub,
    IBlogConnectionRepository blogConnectionRepository)
    : IChatNotificationService
{
    public async Task CreateChatAsync(ChatReadDto chatReadDto, CancellationToken cancellationToken)
    {
        foreach (var chatMember in chatReadDto.Members)
        {
            var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(
                chatMember.Blog.Id, cancellationToken);
            
            foreach (var blogConnection in blogConnections)
            {
                await chatHub.Groups.AddToGroupAsync(blogConnection.ConnectionId, 
                    $"chat_{chatReadDto.Id}", cancellationToken);
            }
        }
        
        await chatHub.Clients.Group($"chat_{chatReadDto.Id}").SendAsync(
            ClientEvents.ChatCreated, chatReadDto, cancellationToken);            
    }
    
    public async Task DeleteChatAsync(ChatReadDto chatReadDto, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"chat_{chatReadDto.Id}").SendAsync(
            ClientEvents.ChatDeleted, chatReadDto, cancellationToken);
        
        foreach (var chatMember in chatReadDto.Members)
        {
            var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(
                chatMember.Blog.Id, cancellationToken);
            
            foreach (var blogConnection in blogConnections)
            {
                await chatHub.Groups.RemoveFromGroupAsync(blogConnection.ConnectionId, 
                    $"chat_{chatReadDto.Id}", cancellationToken);
            }
        }
                                    
        await chatHub.Clients.Group($"chat_{chatReadDto.Id}").SendAsync(
            ClientEvents.ChatDeleted, chatReadDto, cancellationToken);
    }

    public async Task JoinChatsAsync(string connectionId, IEnumerable<ChatReadDto> chats, CancellationToken cancellationToken)
    {
        foreach (var chat in chats)
        {
            await chatHub.Groups.AddToGroupAsync(connectionId, $"chat_{chat.Id}", cancellationToken);
        }
    }
}