using FluentValidation;

namespace Application.UseCases.MessageCases.Commands.SendDelayedMessage;

public class SendDelayedMessageCommandValidator : AbstractValidator<SendDelayedMessageCommand>
{
    public SendDelayedMessageCommandValidator()
    {
        RuleFor(x => x.DateTime)
            .Must(x => x > DateTimeOffset.UtcNow)
            .WithMessage("DateTime must be in the future");
    }
}