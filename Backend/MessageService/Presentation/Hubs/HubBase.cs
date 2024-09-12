using Application.UseCases.ChatCases.Queries.GetBlogChats;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class HubBase(
    IMediator mediator) : Hub
{
    protected string UserId => "123";
    protected Guid BlogId => new Guid(Context.Items[ConnectionPayloads.BlogId].ToString());
    
    public override async Task OnConnectedAsync()
    {
        var blogId = Context.GetHttpContext().Request.Query["blogId"];
        
        if (string.IsNullOrEmpty(blogId))
        {
            throw new HubException("Blog ID is required");
        }

        var blogChatsReadDto = await mediator.Send(new GetBlogChatsQuery
        {
            UserId = UserId,
            BlogId = new Guid(blogId)
        });
        
        Context.Items[ConnectionPayloads.BlogId] = blogId;
        
        foreach (var chat in blogChatsReadDto.Chats)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chat.Id}");
        }
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    private Dictionary<Guid, string> _connections = new Dictionary<Guid, string>();
}