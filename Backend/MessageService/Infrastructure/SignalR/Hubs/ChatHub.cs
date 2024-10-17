using System.Security.Claims;
using Application.UseCases.BlogConnectionCases.Commands.Connect;
using Application.UseCases.BlogConnectionCases.Commands.Disconnect;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Hubs;

[Authorize]
public class ChatHub(
    IMediator mediator)
    : Hub
{
    protected string UserId => Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    protected Guid BlogId => new Guid(GetBlogIdFromQuery());
    
    public override async Task OnConnectedAsync()
    {
        var connectCommand = new ConnectCommand
        {
            BlogId = BlogId,
            UserId = UserId,
            ConnectionId = Context.ConnectionId
        };

        await mediator.Send(connectCommand);
        
        await base.OnConnectedAsync();
    }
    
    private string GetBlogIdFromQuery()
    {
        var blogId = Context.GetHttpContext().Request.Query["userBlogId"];
        
        if (string.IsNullOrEmpty(blogId))
        {
            throw new HubException("Blog ID is required");
        }
    
        return blogId;
    } 
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var disconnectCommand = new DisconnectCommand
        {
            BlogId = BlogId,
            UserId = UserId,
            ConnectionId = Context.ConnectionId
        };
        
        await mediator.Send(disconnectCommand);
        
        await base.OnDisconnectedAsync(exception);
    }
}