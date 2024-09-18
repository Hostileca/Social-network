using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.Connect;

public class ConnectCommand : IRequest
{
    public string ConnectionId { get; set; }
    
    public string UserId { get; set; }
    
    public Guid BlogId { get; set; }
}