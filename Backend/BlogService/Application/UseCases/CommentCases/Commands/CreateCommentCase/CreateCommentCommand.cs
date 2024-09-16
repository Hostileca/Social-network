using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentCommand : IRequest<CommentReadDto>
{
    [JsonIgnore]
    public string? PostId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string BlogId { get; set; }
    public string Content { get; set; }
}