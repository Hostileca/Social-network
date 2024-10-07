using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentCommand : IRequest<CommentReadDto>
{
    [FromRoute]
    [JsonIgnore]
    public string? PostId { get; set; }
    
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [FromQuery]
    [JsonIgnore]
    public string BlogId { get; set; }

    public string Content { get; set; }
}