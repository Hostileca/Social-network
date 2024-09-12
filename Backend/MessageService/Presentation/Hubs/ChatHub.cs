using Application.Dtos;
using Application.UseCases.ChatCases.Commands.CreateChat;
using Application.UseCases.MessageCases.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class ChatHub(
    IMediator mediator) 
    : HubBase(mediator)
{
    public async Task<ChatReadDto> CreateChat(CreateChatCommand createChatCommand)
    {
        createChatCommand.BlogId = BlogId;
        createChatCommand.UserId = UserId;
        
        var chat = await mediator.Send(createChatCommand);
                
        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chat.Id}");
        
        return chat;
    }
}