using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Commands.DeletePostCase;

public class DeletePostByIdCommand : IRequest<PostReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    [JsonIgnore]
    public string? BlogId { get; set; }
    public string PostId { get; set; }
}