using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.Disconnect;

public class DisconnectCommand : IRequest
{
    public string ConnectionId { get; set; }
    
    public string UserId { get; set; }
    
    public Guid BlogId { get; set; }
}