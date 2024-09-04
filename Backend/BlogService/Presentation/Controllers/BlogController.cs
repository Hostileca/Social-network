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
    public async Task<IActionResult> GetAllBlogs([FromQuery]GetAllBlogsQuery getAllBlogsQuery)
    {
        var blogs = await mediator.Send(getAllBlogsQuery);
        
        return Ok(blogs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
    {
        createBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(createBlogCommand);
        
        return Ok(blog);
    }

    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlogById(string blogId)
    {
        var blog = await mediator.Send(new GetBlogByIdQuery{BlogId = blogId});
        
        return Ok(blog);
    }
}