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
        CancellationToken cancellationToken)
    {
        subscribeToBlogCommand.UserBlogId = blogId;
        subscribeToBlogCommand.UserId = UserId;
        
        var subscriptions = await mediator.Send(subscribeToBlogCommand, cancellationToken);
        
        return Ok(subscriptions);
    }

    [HttpDelete("subscriptions")]
    public async Task<IActionResult> RemoveSubscription(string blogId, UnsubscribeFromBlogCommand unsubscribeFromBlogCommand,
        CancellationToken cancellationToken)
    {
        unsubscribeFromBlogCommand.UserBlogId = blogId;
        unsubscribeFromBlogCommand.UserId = UserId;
        
        var subscriptions = await mediator.Send(unsubscribeFromBlogCommand, cancellationToken);
        
        return Ok(subscriptions);
    }
    
    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetBlogSubscriptions(string blogId, CancellationToken cancellationToken)
    {
        var getBlogSubscriptions = new GetBlogSubscriptionsQuery
        {
            BlogId = blogId
        };
        
        var subscribtions = await mediator.Send(getBlogSubscriptions, cancellationToken);
        
        return Ok(subscribtions);
    }
    
    [HttpGet("subscribers")]
    public async Task<IActionResult> GetBlogSubscribers(string blogId, CancellationToken cancellationToken)
    {
        var getBlogSubscribers = new GetBlogSubscribersQuery()
        {
            BlogId = blogId
        };
        
        var subscribtions = await mediator.Send(getBlogSubscribers);
        
        return Ok(subscribtions);
    }
}