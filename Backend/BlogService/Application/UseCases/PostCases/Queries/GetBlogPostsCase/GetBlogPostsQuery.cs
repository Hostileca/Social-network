using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsCase;

public class GetBlogPostsQuery : IRequest<IEnumerable<PostReadDto>>
{
    [JsonIgnore]
    public string BlogId { get; set; }
}