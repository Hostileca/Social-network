using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    public string Username { get; set; }
}