using Application.Dtos;
using Application.UseCases.ChatCases.Commands.CreateChat;
using Application.UseCases.MessageCases.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class MessageHub(
    IMediator mediator)
    : Hub
{
    public async Task<MessageReadDto> SendMessage(SendMessageCommand sendMessageCommand)
    {
        sendMessageCommand.UserId = "123";
        var message = await mediator.Send(sendMessageCommand);
        return message;
    }
    

}