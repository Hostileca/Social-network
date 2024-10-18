using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.LikeCases.Commands.AddLikeToPostCase;

public class AddLikeToPostHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository,
    IBlogRepository blogRepository) 
    : IRequestHandler<AddLikeToPostCommand, PostLikesReadDto>
{
    public async Task<PostLikesReadDto> Handle(AddLikeToPostCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId);
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        
        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.PostId);
        }

        if (post.Likes.Any(x => x.SenderId == request.UserBlogId))
        {
            throw new AlreadyExistsException(typeof(Like).ToString());
        }

        var like = request.Adapt<Like>();
        
        await likeRepository.AddAsync(like, cancellationToken);
        
        await likeRepository.SaveChangesAsync(cancellationToken);
        
        return post.Adapt<PostLikesReadDto>();
    }
}