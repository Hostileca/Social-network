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
    public async Task<IActionResult> GetPosts(string blogId, [FromQuery]GetBlogPostsQuery getBlogPostsQuery, 
        CancellationToken cancellationToken = default)
    {
        getBlogPostsQuery.BlogId = blogId;
        
        var posts = await mediator.Send(getBlogPostsQuery, cancellationToken);
        
        return Ok(posts);
    }
    
    [HttpGet("/blogs/{blogId}/wall")]
    public async Task<IActionResult> GetWall(string blogId, [FromQuery]GetBlogPostsWallQuery getBlogPostsWallQuery, 
        CancellationToken cancellationToken = default)
    {
        getBlogPostsWallQuery.UserBlogId = blogId;
        
        var posts = await mediator.Send(getBlogPostsWallQuery, cancellationToken);
        
        return Ok(posts);
    }
    
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(string blogId, string postId, [FromQuery]DeletePostByIdCommand deletePostByIdCommand,
        CancellationToken cancellationToken = default)
    {
        deletePostByIdCommand.UserId = UserId;
        deletePostByIdCommand.PostId = postId;
        deletePostByIdCommand.BlogId = blogId;
        
        var post = await mediator.Send(deletePostByIdCommand, cancellationToken);
        
        return Ok(post);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost(string blogId, [FromForm]CreatePostCommand createPostCommand,
        CancellationToken cancellationToken = default)
    {
        createPostCommand.UserId = UserId;
        createPostCommand.BlogId = blogId;
        
        var post = await mediator.Send(createPostCommand, cancellationToken);
        
        return Ok(post);
    }
}