using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

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