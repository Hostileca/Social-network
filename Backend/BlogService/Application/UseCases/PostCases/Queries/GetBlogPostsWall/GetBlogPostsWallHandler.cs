using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsWall;

public class GetBlogPostsWallHandler(
    IBlogRepository blogRepository,
    IPostRepository postRepository)
    : IRequestHandler<GetBlogPostsWallQuery, IEnumerable<PostReadDto>>
{
    public async Task<IEnumerable<PostReadDto>> Handle(GetBlogPostsWallQuery request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var pagedFilter = request.Adapt<PagedFilter>();
        
        var posts = await postRepository.GetPostsByBlogSubscriptions(pagedFilter, 
            userBlog.Subscriptions.Select(x => x.SubscribedAtId), cancellationToken);

        return posts.Adapt<IEnumerable<PostReadDto>>();
    }
}