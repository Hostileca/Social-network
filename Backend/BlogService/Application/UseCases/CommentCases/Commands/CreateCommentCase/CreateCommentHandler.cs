using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentHandler(
    ICommentRepository commentRepository,
    IBlogRepository blogRepository,
    IPostRepository postRepository)
    : IRequestHandler<CreateCommentCommand, CommentReadDto>
{
    public async Task<CommentReadDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.BlogId);
        }

        var comment = request.Adapt<Comment>();
        
        await commentRepository.AddAsync(comment, cancellationToken);

        await commentRepository.SaveChangesAsync(cancellationToken);

        return comment.Adapt<CommentReadDto>();
    }
}