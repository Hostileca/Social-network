using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsQuery : IRequest<IEnumerable<BlogReadDto>>
{
    [BindNever]
    public string? UserId { get; set; }
}