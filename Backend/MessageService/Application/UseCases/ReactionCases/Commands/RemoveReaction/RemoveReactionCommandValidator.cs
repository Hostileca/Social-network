using FluentValidation;

namespace Application.UseCases.ReactionCases.Commands.RemoveReaction;

public class RemoveReactionCommandValidator : AbstractValidator<RemoveReactionCommand>
{
    public RemoveReactionCommandValidator()
    {
        RuleFor(x => x.ReactionId)
            .IsGuid();
    }
}