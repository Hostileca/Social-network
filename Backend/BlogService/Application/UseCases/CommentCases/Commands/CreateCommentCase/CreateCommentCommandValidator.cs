using FluentValidation;

namespace Application.UseCases.CommentCases.Commands.CreateCommentCase;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmptyAndNotNull()            
            .MaximumLength(500);

        RuleFor(x => x.BlogId)
            .NotEmptyAndNotNull();
    }
}