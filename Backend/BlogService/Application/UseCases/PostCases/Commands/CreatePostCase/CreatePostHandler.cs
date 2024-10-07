using Application.Configs;
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
    IBlogRepository blogRepository)
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
        
        await postRepository.SaveChangesAsync(cancellationToken);

        var newPostReadDto = post.Adapt<PostReadDto>();
        
        return newPostReadDto;
    }
}