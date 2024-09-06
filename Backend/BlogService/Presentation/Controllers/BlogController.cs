using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Application.UseCases.BlogCases.Queries.GetAllBlogsCase;
using Application.UseCases.BlogCases.Queries.GetBlogByIdCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs")]  
public class BlogController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBlogs(CancellationToken cancellationToken)
    {
        var blogs = await mediator.Send(new GetAllBlogsQuery(), cancellationToken);
        
        return Ok(blogs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand, CancellationToken cancellationToken)
    {
        createBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(createBlogCommand, cancellationToken);
        
        return Ok(blog);
    }

    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlogById(string blogId, CancellationToken cancellationToken)
    {
        var blog = await mediator.Send(new GetBlogByIdQuery{BlogId = blogId}, cancellationToken);
        
        return Ok(blog);
    }
}