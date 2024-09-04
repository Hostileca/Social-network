using Application.UseCases.SubscriptionCases.SubscribeToBlogCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs/{blogId}/subscribers")]  
public class SubscriberController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SubscribeToBlog(string blogId)
    {
        var subscribtions = await mediator.Send(new SubscribeToBlogCommand
        {
            UserBlogId = UserId,
            BlogId = blogId
        });
        
        return Ok(subscribtions);
    }

    [HttpDelete]
    public async Task<IActionResult> UnsubscribeFromBlog(string blogId)
    {
        var subscribtions = await mediator.Send(new SubscribeToBlogCommand
        {
            UserBlogId = UserId,
            BlogId = blogId
        });
        
        return Ok(subscribtions);
    }
}