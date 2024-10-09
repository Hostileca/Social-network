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
    public async Task<IActionResult> AddSubscription(string blogId, [FromBody]SubscribeToBlogCommand subscribeToBlogCommand,
        CancellationToken cancellationToken = default)
    {
        subscribeToBlogCommand.UserId = UserId;
        subscribeToBlogCommand.BlogId = blogId;
        
        var subscription = await mediator.Send(subscribeToBlogCommand, cancellationToken);
        
        return Ok(subscription);
    }

    [HttpDelete("subscriptions/{subscriptionId}")]
    public async Task<IActionResult> RemoveSubscription(string blogId, string subscriptionId, [FromQuery]UnsubscribeFromBlogCommand unsubscribeFromBlogCommand,
        CancellationToken cancellationToken = default)
    {
        unsubscribeFromBlogCommand.UserId = UserId;
        unsubscribeFromBlogCommand.UserBlogId = blogId;
        unsubscribeFromBlogCommand.SubscriptionId = subscriptionId;
        
        var subscription = await mediator.Send(unsubscribeFromBlogCommand, cancellationToken);
        
        return Ok(subscription);
    }
    
    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetBlogSubscriptions(string blogId, [FromQuery]GetBlogSubscriptionsQuery getBlogSubscriptionsQuery, 
        CancellationToken cancellationToken = default)
    {
        getBlogSubscriptionsQuery.BlogId = blogId;
        
        var subscriptions = await mediator.Send(getBlogSubscriptionsQuery, cancellationToken);
        
        return Ok(subscriptions);
    }
    
    [HttpGet("subscribers")]
    public async Task<IActionResult> GetBlogSubscribers(string blogId, [FromQuery]GetBlogSubscribersQuery getBlogSubscribersQuery, 
        CancellationToken cancellationToken = default)
    {
        getBlogSubscribersQuery.BlogId = blogId;
        
        var subscribers = await mediator.Send(getBlogSubscribersQuery, cancellationToken);
        
        return Ok(subscribers);
    }
}