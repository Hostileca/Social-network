using BusinessLogicLayer.Dtos.User;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Please, enter the username");

        RuleFor(x => x.Password)
            .Password();

        RuleFor(x => x.ConfirmPassword)
            .Password()
            .Equal(x => x.Password).WithMessage("The passwords you entered do not match");
    }
}