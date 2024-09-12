﻿using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.PostCases.Commands.DeletePostCase;

public class DeletePostByIdHandler(
    IBlogRepository blogRepository,
    IPostRepository postRepository)
    : IRequestHandler<DeletePostByIdCommand, PostReadDto>
{
    public async Task<PostReadDto> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId ,cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }

        if (post.Owner != blog)
        {
            throw new NoPermissionException("You don't have permission to delete this post");
        }
        
        postRepository.Delete(post);
        return post.Adapt<PostReadDto>();
    }
}