using Application.UseCases.BlogConnectionCases.Commands.AddBlogConnection;
using Application.UseCases.BlogConnectionCases.Commands.DeleteBlogConnection;
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
        var blogId = GetBlogIdFromQuery();
        Context.Items[ConnectionPayloads.BlogId] = blogId;

        await SaveConnectionAsync();
        
        await JoinGroupsAsync();
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await DeleteConnectionAsync();
        
        await base.OnDisconnectedAsync(exception);
    }

    private string GetBlogIdFromQuery()
    {
        var blogId = Context.GetHttpContext().Request.Query["blogId"];
        
        if (string.IsNullOrEmpty(blogId))
        {
            throw new HubException("Blog ID is required");
        }

        return blogId;
    } 

    private async Task JoinGroupsAsync()
    {
        var blogChatsReadDto = await mediator.Send(new GetBlogChatsQuery
        {
            UserId = UserId,
            BlogId = BlogId
        });
        
        foreach (var chat in blogChatsReadDto.Chats)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chat.Id}");
        }
    }

    private async Task SaveConnectionAsync()
    {
        var addConnectionCommand = new AddBlogConnectionCommand
        {
            BlogId = BlogId,
            UserId = UserId,
            ConnectionId = Context.ConnectionId
        };

        await mediator.Send(addConnectionCommand);
    }

    private async Task DeleteConnectionAsync()
    {
        var deleteBlogConnectionCommand = new DeleteBlogConnectionCommand
        {
            BlogId = BlogId,
            ConnectionId = Context.ConnectionId
        };

        await mediator.Send(deleteBlogConnectionCommand);
    }
}