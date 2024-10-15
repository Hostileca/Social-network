using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommand : IRequest<BlogReadDto>
{
    [JsonIgnore]
    [BindNever]
    public string? BlogId { get; set; }
    
    [JsonIgnore]
    [BindNever]
    public string? UserId { get; set; }

    public string Username { get; set; }
    
    public string? Bio { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public IFormFile? MainImage { get; set; }
}