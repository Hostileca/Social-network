using FluentValidation;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Username)
            .IsUsername();

        RuleFor(x => x.BIO)
            .MaximumLength(50);

        RuleFor(x => x.MainImagePath).
            Must(x => x is null || Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("{PropertyName} must be a valid URL");
    }
}