using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.CommentCases.Queries.GetPostCommentsCase;

public class GetPostCommentsQuery : PagedQuery, IRequest<IEnumerable<CommentReadDto>>
{
    [BindNever]
    public string? PostId { get; set; }
}