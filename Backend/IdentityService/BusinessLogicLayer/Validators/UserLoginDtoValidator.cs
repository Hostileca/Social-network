using FluentValidation;
using SharedResources.Dtos.User;

namespace BusinessLogicLayer.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please, enter the password");
    }
}