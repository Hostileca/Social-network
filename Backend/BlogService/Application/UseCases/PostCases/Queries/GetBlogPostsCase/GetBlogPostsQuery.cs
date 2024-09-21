using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Queries.GetBlogPostsCase;

public class GetBlogPostsQuery : IRequest<IEnumerable<PostReadDto>>
{
    [JsonIgnore]
    public string BlogId { get; set; }
}