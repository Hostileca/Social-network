using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.CommentCases.Queries.GetPostCommentsCase;

public class GetPostCommentsQuery : PagedQuery, IRequest<IEnumerable<CommentReadDto>>
{
    [FromRoute]
    public string PostId { get; set; }
}