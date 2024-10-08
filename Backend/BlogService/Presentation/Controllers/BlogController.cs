using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Application.UseCases.BlogCases.Commands.DeleteBlogCase;
using Application.UseCases.BlogCases.Commands.UpdateBlogCase;
using Application.UseCases.BlogCases.Queries.GetAllBlogsCase;
using Application.UseCases.BlogCases.Queries.GetBlogByIdCase;
using Application.UseCases.BlogCases.Queries.GetBlogsByFilterCase;
using Application.UseCases.BlogCases.Queries.GetUserBlogsCase;
using Application.UseCases.LikeCases.Queries.GetBlogLikesCase;
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
    public async Task<IActionResult> GetAllBlogs(GetAllBlogsQuery getAllBlogsQuery,
        CancellationToken cancellationToken = default)
    {
        var blogs = await mediator.Send(getAllBlogsQuery, cancellationToken);
        
        return Ok(blogs);
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> GetAllBlogs([FromQuery]GetBlogsByFilterQuery getBlogsByFilterQuery,
        CancellationToken cancellationToken = default)
    {
        var blogs = await mediator.Send(getBlogsByFilterQuery, cancellationToken);
        
        return Ok(blogs);
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetUserBlogs([FromQuery]GetUserBlogsQuery getUserBlogsQuery,
        CancellationToken cancellationToken = default)
    {
        getUserBlogsQuery.UserId = UserId;
        
        var blogs = await mediator.Send(getUserBlogsQuery, cancellationToken);
        
        return Ok(blogs);
    }
    
    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlogById([FromQuery]GetBlogByIdQuery getBlogByIdQuery, 
        CancellationToken cancellationToken = default)
    {
        var blog = await mediator.Send(getBlogByIdQuery, cancellationToken);
        
        return Ok(blog);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody]CreateBlogCommand createBlogCommand, 
        CancellationToken cancellationToken = default)
    {
        createBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(createBlogCommand, cancellationToken);
        
        return Ok(blog);
    }

    [HttpPatch("{blogId}")]
    public async Task<IActionResult> UpdateBlog([FromBody]UpdateBlogCommand updateBlogCommand,
        CancellationToken cancellationToken = default)
    {
        updateBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(updateBlogCommand, cancellationToken);
        
        return Ok(blog);
    }
    
    [HttpDelete("{blogId}")]
    public async Task<IActionResult> DeleteBlog(DeleteBlogCommand deleteBlogCommand, 
        CancellationToken cancellationToken = default)
    {
        deleteBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(deleteBlogCommand, cancellationToken);
        
        return Ok(blog);
    }
    
    [HttpGet("{blogId}/liked")]
    public async Task<IActionResult> GetLikedPosts(GetBlogLikesQuery getBlogLikesQuery, 
        CancellationToken cancellationToken)
    {
        var posts = await mediator.Send(getBlogLikesQuery, cancellationToken);
        
        return Ok(posts);
    }
    
}