using Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;
using Application.UseCases.PostCases.Commands.CreatePostCase;
using Application.UseCases.PostCases.Commands.DeletePostCase;
using Application.UseCases.PostCases.Queries.GetBlogPostsCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs/{blogId}/posts")] 
public class PostController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPosts(string blogId, CancellationToken cancellationToken)
    {
        var posts = await mediator.Send(new GetBlogPostsQuery
        {
            BlogId = blogId
        }, cancellationToken);
        
        return Ok(posts);
    }
    
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(string blogId, string postId,
        CancellationToken cancellationToken)
    {
        var deletePostCommand = new DeletePostByIdCommand
        {
            UserId = UserId,
            PostId = postId,
            BlogId = blogId
        };
        
        var post = await mediator.Send(deletePostCommand, cancellationToken);
        
        return Ok(post);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost(string blogId, [FromForm]CreatePostCommand createPostCommand,
        CancellationToken cancellationToken)
    {
        createPostCommand.UserId = UserId;
        createPostCommand.BlogId = blogId;
        var post = await mediator.Send(createPostCommand, cancellationToken);
        
        return Ok(post);
    }
}