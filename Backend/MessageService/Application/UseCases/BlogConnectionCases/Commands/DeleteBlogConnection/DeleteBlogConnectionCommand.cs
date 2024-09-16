using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.DeleteBlogConnection;

public class DeleteBlogConnectionCommand : IRequest<BlogConnectionReadDto>
{
    public Guid BlogId { get; set; }
    public string? ConnectionId { get; set; }
}