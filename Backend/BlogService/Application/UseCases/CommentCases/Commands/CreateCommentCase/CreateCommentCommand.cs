using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentCommand : IRequest<CommentReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? PostId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public string UserBlogId { get; set; }

    public string Content { get; set; }
}