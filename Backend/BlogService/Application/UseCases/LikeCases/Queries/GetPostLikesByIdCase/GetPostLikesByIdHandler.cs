using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;

public class GetPostLikesByIdHandler(
    IPostRepository postRepository)
    : IRequestHandler<GetPostLikesByIdQuery, PostLikesReadDto>
{
    public async Task<PostLikesReadDto> Handle(GetPostLikesByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.Id, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString());
        }

        return post.Adapt<PostLikesReadDto>();
    }
}