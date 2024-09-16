using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.LikeCases.Commands.AddLikeToPostCase;

public class AddLikeToPostCommand : IRequest<PostLikesReadDto>
{
    [JsonIgnore]
    public string? PostId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string BlogId { get; set; }
}