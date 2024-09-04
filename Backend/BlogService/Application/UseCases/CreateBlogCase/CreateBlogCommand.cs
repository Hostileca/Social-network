using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.CreateBlogCase;

public class CreateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string? Username { get; set; }
}