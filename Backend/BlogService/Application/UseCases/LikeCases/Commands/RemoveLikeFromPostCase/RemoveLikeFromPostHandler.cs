using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostHandler(
    IPostRepository postRepository,
    IBlogRepository blogRepository,
    ILikeRepository likeRepository) : IRequestHandler<RemoveLikeFromPostCommand, PostLikesReadDto>
{
    public async Task<PostLikesReadDto> Handle(RemoveLikeFromPostCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (blog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        
        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }

        var like = post.Likes.FirstOrDefault(x => x.SenderId == request.BlogId);
        
        if (like is null)
        {
            throw new NotFoundException(typeof(Like).ToString());
        }
        
        likeRepository.Delete(like);
        
        await likeRepository.SaveChangesAsync(cancellationToken);
        
        return post.Adapt<PostLikesReadDto>();
    }
}