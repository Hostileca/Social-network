using Application.UseCases.PostCases.Commands.CreatePostCase;
using Application.UseCases.PostCases.Queries.GetBlogPostsCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("blogs/{blogId}/posts")] 
public class PostController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePost(string blogId, [FromForm]CreatePostCommand createPostCommand)
    {
        createPostCommand.UserId = UserId;
        createPostCommand.BlogId = blogId;
        var post = await mediator.Send(createPostCommand);
        
        return Ok(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts(string blogId)
    {
        var posts = await mediator.Send(new GetBlogPostsQuery()
        {
            BlogId = blogId
        });
        
        return Ok(posts);
    }
}