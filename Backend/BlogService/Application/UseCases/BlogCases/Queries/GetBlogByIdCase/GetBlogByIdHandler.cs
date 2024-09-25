﻿using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;
using StackExchange.Redis;

namespace Application.UseCases.BlogCases.Queries.GetBlogByIdCase;

public class GetBlogByIdHandler(
    IBlogRepository blogRepository,
    ICacheRepository cacheRepository) 
    : IRequestHandler<GetBlogByIdQuery, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedBlog = await cacheRepository.GetAsync<BlogReadDto>(request.BlogId);
        
        if (cachedBlog is not null)
        {
            return cachedBlog;
        }
        
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }

        var blogReadDto = blog.Adapt<BlogReadDto>();

        await cacheRepository.SetAsync(blogReadDto.Id.ToString(), blogReadDto, CacheConfig.BlogCacheTime);
        
        return blogReadDto;
    }
}