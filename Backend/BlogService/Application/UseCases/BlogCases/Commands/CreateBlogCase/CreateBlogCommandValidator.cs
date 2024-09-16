using FluentValidation;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(x => x.Username)
            .IsUsername();
    }
}