using Application.UseCases.MessageCases.Commands.SendMessage;
using FluentValidation;

namespace Application.UseCases.MessageCases.Commands;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public const int MaxTextLength = 2000;
    
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmptyAndNotNull()
            .MaximumLength(MaxTextLength);
    }
}