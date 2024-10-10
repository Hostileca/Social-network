using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetPostLikesByIdCase;

public class GetLikeSendersByPostIdQuery : PagedQuery, IRequest<IEnumerable<LikeSenderReadDto>>
{
    [BindNever]
    public string? PostId { get; set; }
}