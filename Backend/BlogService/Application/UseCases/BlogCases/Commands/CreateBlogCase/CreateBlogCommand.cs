using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    public string Username { get; set; }
}