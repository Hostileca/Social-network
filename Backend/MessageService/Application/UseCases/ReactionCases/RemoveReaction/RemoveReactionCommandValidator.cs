using FluentValidation;

namespace Application.UseCases.ReactionCases.RemoveReaction;

public class RemoveReactionCommandValidator : AbstractValidator<RemoveReactionCommand>
{
    public RemoveReactionCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();

        RuleFor(x => x.ReactionId)
            .IsGuid();
    }
}