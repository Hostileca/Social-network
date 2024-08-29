using BusinessLogicLayer.ViewModels;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please, enter the password");
    }
}