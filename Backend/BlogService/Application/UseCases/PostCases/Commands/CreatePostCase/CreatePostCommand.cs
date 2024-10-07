using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Commands.CreatePostCase;

public class CreatePostCommand : IRequest<PostReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }   
    
    [FromRoute]
    [JsonIgnore]
    public string? BlogId { get; set; }
    
    [FromForm]
    public string? Content { get; set; }
    
    [FromForm]
    public IEnumerable<IFormFile>? Attachments { get; set; }
}