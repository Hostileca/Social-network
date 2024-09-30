using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Commands.SendMessage;

public class SendMessageCommand : SendMessageCommandBase, IRequest<MessageReadDto>
{

}