using FluentValidation;

namespace Application.UseCases.PostCases.Commands.CreatePostCase;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content)
            .MaximumLength(1000);
    }
}