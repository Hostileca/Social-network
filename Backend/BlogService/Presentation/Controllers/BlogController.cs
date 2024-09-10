using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Application.UseCases.BlogCases.Commands.DeleteBlogCase;
using Application.UseCases.BlogCases.Commands.UpdateBlogCase;
using Application.UseCases.BlogCases.Queries.GetAllBlogsCase;
using Application.UseCases.BlogCases.Queries.GetBlogByIdCase;
using Application.UseCases.BlogCases.Queries.GetUserBlogsCase;
using Application.UseCases.LikeCases.Queries.GetBlogLikesCase;
using Application.UseCases.PostCases.Commands.CreatePostCase;
using Application.UseCases.PostCases.Queries.GetBlogPostsCase;
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
    
    [HttpGet("me")]
    public async Task<IActionResult> GetUserBlogs(CancellationToken cancellationToken)
    {
        var blogs = await mediator.Send(new GetUserBlogsQuery{UserId = UserId}, cancellationToken);
        
        return Ok(blogs);
    }
    
    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlogById(string blogId, 
        CancellationToken cancellationToken)
    {
        var blog = await mediator.Send(new GetBlogByIdQuery{BlogId = blogId}, cancellationToken);
        
        return Ok(blog);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand, 
        CancellationToken cancellationToken)
    {
        createBlogCommand.UserId = UserId;
        
        var blog = await mediator.Send(createBlogCommand, cancellationToken);
        
        return Ok(blog);
    }

    [HttpPut("{blogId}")]
    public async Task<IActionResult> UpdateBlog(string blogId, UpdateBlogCommand updateBlogCommand,
        CancellationToken cancellationToken)
    {
        var blog = await mediator.Send(updateBlogCommand, cancellationToken);
        
        return Ok(blog);
    }
    
    [HttpDelete("{blogId}")]
    public async Task<IActionResult> DeleteBlog(string blogId, 
        CancellationToken cancellationToken)
    {
        var blog = await mediator.Send(new DeleteBlogCommand{BlogId = blogId}, cancellationToken);
        
        return Ok(blog);
    }
    
    [HttpGet("{blogId}/liked")]
    public async Task<IActionResult> GetLikedPosts(string blogId, 
        CancellationToken cancellationToken)
    {
        var posts = await mediator.Send(new GetBlogLikesQuery{BlogId = blogId}, cancellationToken);
        
        return Ok(posts);
    }
    
}