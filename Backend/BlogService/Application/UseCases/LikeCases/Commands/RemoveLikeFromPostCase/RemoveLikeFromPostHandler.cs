using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostHandler(
    IPostRepository postRepository,
    IBlogRepository blogRepository,
    ILikeRepository likeRepository) : IRequestHandler<RemoveLikeFromPostCommand, PostLikesReadDto>
{
    public async Task<PostLikesReadDto> Handle(RemoveLikeFromPostCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        
        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.PostId);
        }

        var like = post.Likes.FirstOrDefault(x => x.SenderId == request.BlogId);
        
        if (like is null)
        {
            throw new NotFoundException(typeof(Like).ToString(), request.PostId);
        }
        
        likeRepository.Delete(like);
        
        await likeRepository.SaveChangesAsync(cancellationToken);
        
        return post.Adapt<PostLikesReadDto>();
    }
}