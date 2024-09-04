using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? BlogId { get; set; }    
}