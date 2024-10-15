using FluentValidation;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Username)
            .MaximumLength(20);

        RuleFor(x => x.Bio)
            .MaximumLength(70);

        RuleFor(x => x.MainImage);
    }
}