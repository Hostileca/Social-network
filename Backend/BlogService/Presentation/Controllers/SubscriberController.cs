using Application.UseCases.SubscribeToBlogCase;
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
            UserBlogId = "66d851d71643143a6d587b8a",
            BlogId = blogId
        });
        
        return Ok(subscribtions);
    }
}