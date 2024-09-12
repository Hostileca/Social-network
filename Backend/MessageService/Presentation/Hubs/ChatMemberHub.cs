using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class ChatMemberHub(
    IMediator mediator)
    : Hub
{
    
}