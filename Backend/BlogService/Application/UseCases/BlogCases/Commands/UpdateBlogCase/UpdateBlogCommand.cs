using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    public string? Id { get; set; }
    
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public string Username { get; set; }
    
    public string? BIO { get; set; }
    
    public string? MainImagePath { get; set; }
}