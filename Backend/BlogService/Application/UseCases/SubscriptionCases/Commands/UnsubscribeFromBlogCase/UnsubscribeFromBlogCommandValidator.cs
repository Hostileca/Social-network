using FluentValidation;

namespace Application.UseCases.SubscriptionCases.Commands.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommandValidator : AbstractValidator<UnsubscribeFromBlogCommand>
{
    public UnsubscribeFromBlogCommandValidator()
    {
        RuleFor(x => x.UnSubscribeFromId)
            .NotEmptyAndNotNull();
    }
}