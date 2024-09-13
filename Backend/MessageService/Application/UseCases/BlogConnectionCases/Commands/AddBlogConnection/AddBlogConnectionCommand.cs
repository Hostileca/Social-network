using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.AddBlogConnection;

public class AddBlogConnectionCommand : IRequest<BlogConnectionReadDto>
{
    public string? UserId { get; set; }
    
    public Guid BlogId { get; set; }
    
    public string? ConnectionId { get; set; }
}