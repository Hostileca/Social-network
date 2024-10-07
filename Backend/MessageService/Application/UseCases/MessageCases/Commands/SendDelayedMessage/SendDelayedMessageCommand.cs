using System.Text.Json.Serialization;
using Application.UseCases.MessageCases.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Commands.SendDelayedMessage;

public class SendDelayedMessageCommand : SendMessageCommandBase, IRequest<DelayedMessageReadDto>
{
    [FromForm]
    public DateTimeOffset Delay { get; set; }
}