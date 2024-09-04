using Application.Dtos;
using Application.UseCases;
using Application.UseCases.CreateBlogCase;
using Application.UseCases.GetAllBlogsCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        createBlogCommand.UserId = Guid.NewGuid();
        var blog = await mediator.Send(createBlogCommand);
        return Ok(blog);
    }
}