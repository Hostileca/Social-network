﻿using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.PostCases.Commands.CreatePostCase;

public class CreatePostHandler(
    IPostRepository postRepository,
    IBlogRepository blogRepository,
    IAttachmentRepository attachmentRepository,
    ICacheRepository cacheRepository)
    : IRequestHandler<CreatePostCommand, PostReadDto>
{
    public async Task<PostReadDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId ,cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var post = request.Adapt<Post>();
        
        await postRepository.AddAsync(post, cancellationToken);
        
        if(request.Attachments is not null)
        {
            foreach (var file in request.Attachments)
            {
                await attachmentRepository.AddAsync(
                    new Attachment
                    {
                        Id = Guid.NewGuid().ToString(), 
                        Data = await ConvertToBase64Async(file, cancellationToken),
                        Post = post,
                        ContentType = MimeTypes.GetMimeType(file.FileName)
                    }, 
                    cancellationToken);
            }
        }
        await postRepository.SaveChangesAsync(cancellationToken);

        var newPostReadDto = post.Adapt<PostReadDto>();
        
        await cacheRepository.SetAsync(blog.Id, blog.Posts.Adapt<IEnumerable<PostReadDto>>(), CacheConfig.PostCacheTime);
        
        return newPostReadDto;
    }
    
    private static async Task<string> ConvertToBase64Async(IFormFile formFile, CancellationToken cancellationToken)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return string.Empty;
        }

        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream, cancellationToken);
        var fileBytes = memoryStream.ToArray();

        return Convert.ToBase64String(fileBytes);
    }
}