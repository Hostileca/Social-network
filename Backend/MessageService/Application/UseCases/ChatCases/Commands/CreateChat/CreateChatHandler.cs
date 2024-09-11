using Application.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatHandler(
    IChatRepository chatRepository)
    : IRequestHandler<CreateChatCommand, ChatReadDto>
{
    public Task<ChatReadDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    { 
        throw new NotImplementedException();
    }
}