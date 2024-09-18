using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? BlogId { get; set; }    
    
    [JsonIgnore]
    public string? UserId { get; set; }
}