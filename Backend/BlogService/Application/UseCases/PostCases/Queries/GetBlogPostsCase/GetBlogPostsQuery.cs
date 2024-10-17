using System.Text.Json.Serialization;
using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsCase;

public class GetBlogPostsQuery : PagedQuery, IRequest<IEnumerable<PostReadDto>>
{
    [BindNever]
    public string? BlogId { get; set; }
}