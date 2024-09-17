using FluentValidation;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionCommandValidator : AbstractValidator<SendReactionCommand>
{
    public SendReactionCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();

        RuleFor(x => x.Emoji)
            .IsEmoji();
    }
}