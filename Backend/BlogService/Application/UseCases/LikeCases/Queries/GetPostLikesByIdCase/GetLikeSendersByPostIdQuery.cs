using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;

public class GetLikeSendersByPostIdQuery : PagedQuery, IRequest<IEnumerable<LikeSenderReadDto>>
{
    [FromRoute]
    public string PostId { get; set; }
}