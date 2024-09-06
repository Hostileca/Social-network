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
    [HttpPost("subscribers")]
    public async Task<IActionResult> SubscribeToBlog(string blogId, SubscribeToBlogCommand subscribeToBlogCommand)
    {
        subscribeToBlogCommand.SubscribeAtId = blogId;
        subscribeToBlogCommand.UserId = UserId;
        
        var subscribtions = await mediator.Send(subscribeToBlogCommand);
        
        return Ok(subscribtions);
    }

    [HttpDelete("subscribers")]
    public async Task<IActionResult> UnsubscribeFromBlog(string blogId, UnsubscribeFromBlogCommand unsubscribeFromBlogCommand)
    {
        unsubscribeFromBlogCommand.SubscripeAtId = blogId;
        unsubscribeFromBlogCommand.UserId = UserId;
        
        var subscribtions = await mediator.Send(unsubscribeFromBlogCommand);
        
        return Ok(subscribtions);
    }
    
    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetBlogSubscriptions(string blogId)
    {
        var getBlogSubscriptions = new GetBlogSubscriptionsQuery
        {
            BlogId = blogId
        };
        
        var subscribtions = await mediator.Send(getBlogSubscriptions);
        
        return Ok(subscribtions);
    }
    
    [HttpGet("subscribers")]
    public async Task<IActionResult> GetBlogSubscribers(string blogId)
    {
        var getBlogSubscribers = new GetBlogSubscribersQuery()
        {
            BlogId = blogId
        };
        
        var subscribtions = await mediator.Send(getBlogSubscribers);
        
        return Ok(subscribtions);
    }
}