using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetBlogLikesCase;

public class GetBlogLikesQuery : PagedQuery, IRequest<IEnumerable<PostReadDto>>
{
    [BindNever]
    public string BlogId { get; set; }
}