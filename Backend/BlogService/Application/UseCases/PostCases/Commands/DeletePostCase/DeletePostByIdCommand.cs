using System.Text.Json.Serialization;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.PostCases.Commands.DeletePostCase;

public class DeletePostByIdCommand : IRequest<PostReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    [JsonIgnore]
    public string? BlogId { get; set; }
    public string PostId { get; set; }
}