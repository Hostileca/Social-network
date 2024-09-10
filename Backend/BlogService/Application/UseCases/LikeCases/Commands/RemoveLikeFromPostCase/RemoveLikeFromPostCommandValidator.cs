using FluentValidation;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostCommandValidator : AbstractValidator<RemoveLikeFromPostCommand>
{
    public RemoveLikeFromPostCommandValidator()
    {
        RuleFor(x => x.BlogId)
            .NotEmptyAndNotNull();
    }
}