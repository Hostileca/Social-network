using FluentValidation;

namespace Application.UseCases.MessageCases.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public const int MaxTextLength = 2000;
    
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();

        RuleFor(x => x.Text)
            .NotEmptyAndNotNull()
            .MaximumLength(MaxTextLength);
    }
}