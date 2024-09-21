using FluentValidation;
using SharedResources.Dtos.User;

namespace BusinessLogicLayer.Validators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Please, enter the username");

        RuleFor(x => x.Password)
            .Password();

        RuleFor(x => x.ConfirmPassword)
            .Password()
            .Equal(x => x.Password).WithMessage("The passwords you entered do not match");
    }
}