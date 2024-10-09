using System.Text.Json.Serialization;
using Application.UseCases.Base.Queries.Paged;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsWall;

public class GetBlogPostsWallQuery : PagedQuery, IRequest<IEnumerable<PostReadDto>>
{
    [BindNever]
    public string UserId { get; set; }
    
    [BindNever]
    public string UserBlogId { get; set; }
}