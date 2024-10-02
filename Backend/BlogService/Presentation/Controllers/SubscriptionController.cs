using Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
using Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;
using Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;
using Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs/{blogId}")]  
public class SubscriptionController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpPost("subscriptions")]
    public async Task<IActionResult> AddSubscription(string blogId, SubscribeToBlogCommand subscribeToBlogCommand,
        CancellationToken cancellationToken = default)
    {
        subscribeToBlogCommand.UserBlogId = blogId;
        subscribeToBlogCommand.UserId = UserId;
        
        var subscription = await mediator.Send(subscribeToBlogCommand, cancellationToken);
        
        return Ok(subscription);
    }

    [HttpDelete("subscriptions/{id}")]
    public async Task<IActionResult> RemoveSubscription(string blogId, string id,
        CancellationToken cancellationToken = default)
    {
        var unsubscribeFromBlogCommand = new UnsubscribeFromBlogCommand
        {
            UserBlogId = blogId,
            Id = id,
            UserId = UserId
        };
        
        var subscription = await mediator.Send(unsubscribeFromBlogCommand, cancellationToken);
        
        return Ok(subscription);
    }
    
    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetBlogSubscriptions(string blogId, 
        CancellationToken cancellationToken = default)
    {
        var getBlogSubscriptions = new GetBlogSubscriptionsQuery
        {
            BlogId = blogId
        };
        
        var subscribtion = await mediator.Send(getBlogSubscriptions, cancellationToken);
        
        return Ok(subscribtion);
    }
    
    [HttpGet("subscribers")]
    public async Task<IActionResult> GetBlogSubscribers(string blogId, 
        CancellationToken cancellationToken = default)
    {
        var getBlogSubscribers = new GetBlogSubscribersQuery()
        {
            BlogId = blogId
        };
        
        var subscribtions = await mediator.Send(getBlogSubscribers);
        
        return Ok(subscribtions);
    }
}