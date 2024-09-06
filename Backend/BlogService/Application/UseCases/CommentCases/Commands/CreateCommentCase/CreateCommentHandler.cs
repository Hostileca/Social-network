using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentHandler(
    ICommentRepository commentRepository,
    IBlogRepository blogRepository,
    IPostRepository postRepository)
    : IRequestHandler<CreateCommentCommand, CommentReadDto>
{
    public async Task<CommentReadDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (blog.UserId != request.UserId)
        {
            throw new UnauthorizedException("It is not your blog");
        }
        
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }

        var comment = request.Adapt<Comment>();
        
        await commentRepository.AddAsync(comment, cancellationToken);

        await commentRepository.SaveChangesAsync(cancellationToken);

        return comment.Adapt<CommentReadDto>();
    }
}