using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;

public class GetPostLikesByIdQuery : IRequest<PostLikesReadDto>
{
    public string Id { get; set; }
}