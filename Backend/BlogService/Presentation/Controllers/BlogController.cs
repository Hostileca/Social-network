using Application.Dtos;
using Application.UseCases;
using Application.UseCases.CreateBlogCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs")]  
public class BlogController(
    IMediator mediator) 
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetByIdQuery<BlogReadDto>{ Id = id };
        var userDto = await mediator.Send(query);
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
    {
        createBlogCommand.UserId = Guid.NewGuid();
        var blog = await mediator.Send(createBlogCommand);
        return Ok(blog);
    }
}