using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetBlogLikesCase;

public class GetBlogLikesQuery : PagedQuery, IRequest<IEnumerable<PostReadDto>>
{
    [FromRoute]
    public string BlogId { get; set; }
}