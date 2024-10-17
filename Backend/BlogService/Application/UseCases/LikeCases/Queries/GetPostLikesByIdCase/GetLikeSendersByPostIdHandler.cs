using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;

public class GetLikeSendersByPostIdHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository)
    : IRequestHandler<GetLikeSendersByPostIdQuery, IEnumerable<LikeSenderReadDto>>
{
    public async Task<IEnumerable<LikeSenderReadDto>> Handle(GetLikeSendersByPostIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Post).ToString(), request.PostId);
        }

        var pagedFilter = request.Adapt<PagedFilter>();
        
        var likes = await likeRepository.GetLikeSendersByPostIdAsync(pagedFilter, request.PostId,
            cancellationToken);
        return likes.Adapt<IEnumerable<LikeSenderReadDto>>();
    }
}