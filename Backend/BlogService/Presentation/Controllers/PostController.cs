using Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;
using Application.UseCases.PostCases.Commands.CreatePostCase;
using Application.UseCases.PostCases.Commands.DeletePostCase;
using Application.UseCases.PostCases.Queries.GetBlogPostsCase;
using Application.UseCases.PostCases.Queries.GetBlogPostsWall;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs/{blogId}/posts")] 
public class PostController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPosts([FromQuery]GetBlogPostsQuery getBlogPostsQuery, 
        CancellationToken cancellationToken = default)
    {
        var posts = await mediator.Send(getBlogPostsQuery, cancellationToken);
        
        return Ok(posts);
    }
    
    [HttpGet("/blogs/{blogId}/wall")]
    public async Task<IActionResult> GetWall([FromQuery]GetBlogPostsWallQuery getBlogPostsWallQuery, 
        CancellationToken cancellationToken = default)
    {
        var posts = await mediator.Send(getBlogPostsWallQuery, cancellationToken);
        
        return Ok(posts);
    }
    
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost([FromQuery]DeletePostByIdCommand deletePostByIdCommand,
        CancellationToken cancellationToken = default)
    {
        deletePostByIdCommand.UserId = UserId;
        
        var post = await mediator.Send(deletePostByIdCommand, cancellationToken);
        
        return Ok(post);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm]CreatePostCommand createPostCommand,
        CancellationToken cancellationToken = default)
    {
        createPostCommand.UserId = UserId;
        var post = await mediator.Send(createPostCommand, cancellationToken);
        
        return Ok(post);
    }
}