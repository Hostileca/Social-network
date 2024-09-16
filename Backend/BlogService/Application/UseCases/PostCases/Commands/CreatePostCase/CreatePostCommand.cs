using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.PostCases.Commands.CreatePostCase;

public class CreatePostCommand : IRequest<PostReadDto>
{
    public string Content { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }    
    [JsonIgnore]
    public string? BlogId { get; set; }
    public IEnumerable<IFormFile>? Attachments { get; set; }
}