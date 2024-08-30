using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries;

namespace Presentation.Controllers;

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
}