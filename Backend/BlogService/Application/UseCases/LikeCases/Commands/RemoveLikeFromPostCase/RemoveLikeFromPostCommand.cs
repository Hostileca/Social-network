using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostCommand : IRequest<PostLikesReadDto>
{
    [JsonIgnore]
    public string? PostId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string BlogId { get; set; }
}