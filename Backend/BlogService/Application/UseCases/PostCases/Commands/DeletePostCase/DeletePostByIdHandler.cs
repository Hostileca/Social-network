using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
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
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }
        
        if (post.OwnerId != request.BlogId)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }
        
        if (post.Owner.UserId != request.UserId)
        {
            throw new UnauthorizedException("It is not your post");
        }
        
        postRepository.Delete(post);
        return post.Adapt<PostReadDto>();
    }
}