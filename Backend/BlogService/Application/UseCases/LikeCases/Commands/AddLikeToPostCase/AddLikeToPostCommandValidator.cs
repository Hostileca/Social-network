using FluentValidation;

namespace Application.UseCases.LikeCases.Commands.AddLikeToPostCase;

public class AddLikeToPostCommandValidator : AbstractValidator<AddLikeToPostCommand>
{
    public AddLikeToPostCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .NotEmptyAndNotNull();
    }
}