using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

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
            throw new NotFoundException(typeof(Post).ToString());
        }

        return post.Comments.Adapt<IEnumerable<CommentReadDto>>();
    }
}