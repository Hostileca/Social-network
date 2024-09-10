using FluentValidation;

namespace Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;

public class SubscribeToBlogCommandValidator : AbstractValidator<SubscribeToBlogCommand>
{
    public SubscribeToBlogCommandValidator()
    {
        RuleFor(x => x.SubscribeAtId)
            .NotEmptyAndNotNull();
    }
}