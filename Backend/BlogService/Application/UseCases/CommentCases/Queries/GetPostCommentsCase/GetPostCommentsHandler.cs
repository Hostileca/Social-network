using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.CommentCases.Queries.GetPostCommentsCase;

public class GetPostCommentsHandler(
    IPostRepository postRepository)
    : IRequestHandler<GetPostCommentsQuery, IEnumerable<CommentReadDto>>
{
    public async Task<IEnumerable<CommentReadDto>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.PostId.ToString());
        }

        return post.Comments.Adapt<IEnumerable<CommentReadDto>>();
    }
}