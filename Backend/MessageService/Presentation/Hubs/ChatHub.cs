using Application.Dtos;
using Application.UseCases.BlogConnectionCases.Queries.GetBlogConnectionsByBlogId;
using Application.UseCases.ChatCases.Commands.CreateChat;
using Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;
using Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;
using Application.UseCases.MessageCases.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class ChatHub(
    IMediator mediator) 
    : HubBase(mediator)
{
    public async Task CreateChat(CreateChatCommand createChatCommand)
    {
        createChatCommand.BlogId = BlogId;
        createChatCommand.UserId = UserId;
        
        var chat = await mediator.Send(createChatCommand);

        await ConnectMembersToChatAsync(chat);
        
        await Clients.Group($"chat_{chat.Id}").SendAsync(ClientEvents.ChatCreated, chat);
    }

    public async Task AddMemberToChat(AddMemberToChatCommand addMemberToChatCommand)
    {
        addMemberToChatCommand.UserBlogId = BlogId;
        addMemberToChatCommand.UserId = UserId;
        
        var member = await mediator.Send(addMemberToChatCommand);

        await ConnectMemberToChatAsync(member, addMemberToChatCommand.ChatId);
        
        await Clients.Group($"chat_{addMemberToChatCommand.ChatId}").SendAsync(ClientEvents.ChatMemberAdded, member);
    }

    public async Task RemoveMemberFromChat(RemoveMemberFromChatCommand removeMemberFromChatCommand)
    {
        removeMemberFromChatCommand.UserBlogId = BlogId;
        removeMemberFromChatCommand.UserId = UserId;
        
        var member = await mediator.Send(removeMemberFromChatCommand);
        
        await DisconnectMemberFromChatAsync(member, removeMemberFromChatCommand.ChatId);
        
        await Clients.Group($"chat_{removeMemberFromChatCommand.ChatId}").SendAsync(ClientEvents.ChatMemberRemoved, member);
    }

    public async Task SendMessage(SendMessageCommand sendMessageCommand)
    {
        sendMessageCommand.UserBlogId = BlogId;
        sendMessageCommand.UserId = UserId;
        
        await mediator.Send(sendMessageCommand);
        
        await Clients.Group($"chat_{sendMessageCommand.ChatId}").SendAsync(ClientEvents.MessageSent, sendMessageCommand);
    }
    
    private async Task ConnectMembersToChatAsync(ChatReadDto chat)
    {
        foreach (var chatMember in chat.Members)
        {
            await ConnectMemberToChatAsync(chatMember, chat.Id);
        }
    }

    private async Task ConnectMemberToChatAsync(ChatMemberReadDto chatMember, Guid chatId)
    {
        var connections = await mediator.Send(new GetBlogConnectionsByBlogIdCommand
        {
            BlogId = chatMember.Blog.Id,
        });
            
        foreach (var connection in connections)
        {
            await Groups.AddToGroupAsync(connection.ConnectionId, $"chat_{chatId}");
        }
    }
    
    private async Task DisconnectMemberFromChatAsync(ChatMemberReadDto chatMember, Guid chatId)
    {
        var connections = await mediator.Send(new GetBlogConnectionsByBlogIdCommand
        {
            BlogId = chatMember.Blog.Id,
        });
            
        foreach (var connection in connections)
        {
            await Groups.RemoveFromGroupAsync(connection.ConnectionId, $"chat_{chatId}");
        }
    }
}