using BusinessLogicLayer.ViewModels;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Please, enter the username");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please, enter the password")
            .Length(4, 15).WithMessage("Password must be between 4 and 15 characters");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Please, reenter the password")
            .Equal(x => x.Password).WithMessage("The passwords you entered do not match")
            .Length(4, 15).WithMessage("Password must be between 4 and 15 characters");
    }
}