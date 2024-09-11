using Application.Dtos;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.DeleteChat;

public class DeleteChatCommand : IRequest<ChatReadDto>
{
    public string UserId { get; set; }
    
    public Guid BlogId { get; set; }
    
    public Guid ChatId { get; set; }
}