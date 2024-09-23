using System.Text.Json.Serialization;
using Application.UseCases.MessageCases.Commands.SendMessage;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Commands.SendDelayedMessage;

public class SendDelayedMessageCommand : SendMessageCommandBase, IRequest<DelayedMessageReadDto>
{
    public DateTimeOffset Delay { get; set; }
}