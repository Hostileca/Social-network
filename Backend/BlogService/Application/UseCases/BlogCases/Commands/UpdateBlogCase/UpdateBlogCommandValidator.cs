using FluentValidation;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Username)
            .IsUsername();

        RuleFor(x => x.Bio)
            .MaximumLength(70);

        RuleFor(x => x.MainImage);
    }
}