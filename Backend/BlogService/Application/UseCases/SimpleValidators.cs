using FluentValidation;

namespace Application.UseCases;

public static class SimpleValidators
{
    public static IRuleBuilder<T, string> NotEmptyAndNotNull<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotNull().WithMessage("{PropertyName} cannot be null")
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");
    }

    public static IRuleBuilder<T, string> IsUsername<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmptyAndNotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters")
            .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
    }
}