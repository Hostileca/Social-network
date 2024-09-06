using System.Text.Json.Serialization;
using MediatR;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostCommand : IRequest<int>
{
    [JsonIgnore]
    public string? PostId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public string BlogId { get; set; }
}