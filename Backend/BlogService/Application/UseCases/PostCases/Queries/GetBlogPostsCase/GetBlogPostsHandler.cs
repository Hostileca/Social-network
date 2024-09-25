using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsCase;

public class GetBlogPostsHandler(
    IBlogRepository blogRepository,
    ICacheRepository cacheRepository)
    : IRequestHandler<GetBlogPostsQuery, IEnumerable<PostReadDto>>
{
    public async Task<IEnumerable<PostReadDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var cachedPosts = await cacheRepository.GetAsync<IEnumerable<PostReadDto>>(request.BlogId);
        
        if (cachedPosts is not null)
        {
            return cachedPosts;
        }
        
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }

        var postsReadDto = blog.Posts.Adapt<IEnumerable<PostReadDto>>();
        
        await cacheRepository.SetAsync(request.BlogId, postsReadDto, CacheConfig.PostCacheTime);
        
        return postsReadDto;
    }
}