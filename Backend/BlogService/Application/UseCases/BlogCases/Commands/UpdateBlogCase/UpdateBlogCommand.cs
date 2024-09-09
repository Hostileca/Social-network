using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? Id { get; set; }
    public string Username { get; set; }
    public string? BIO { get; set; }
    public string? MainImagePath { get; set; }
}