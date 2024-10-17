using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.CommentCases.Queries.GetPostCommentsCase;

public class GetPostCommentsHandler(
    IPostRepository postRepository,
    ICommentRepository commentRepository)
    : IRequestHandler<GetPostCommentsQuery, IEnumerable<CommentReadDto>>
{
    public async Task<IEnumerable<CommentReadDto>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.PostId.ToString());
        }

        var pagedFilter = request.Adapt<PagedFilter>();
        
        var comments = await commentRepository.GetCommentsByPostId(pagedFilter, 
            request.PostId, cancellationToken);
        
        return comments.Adapt<IEnumerable<CommentReadDto>>();
    }
}