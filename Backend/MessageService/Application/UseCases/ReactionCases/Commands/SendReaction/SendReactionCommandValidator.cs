using FluentValidation;

namespace Application.UseCases.ReactionCases.Commands.SendReaction;

public class SendReactionCommandValidator : AbstractValidator<SendReactionCommand>
{
    public SendReactionCommandValidator()
    {
        RuleFor(x => x.Emoji)
            .IsEmoji();
    }
}