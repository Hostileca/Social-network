using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;

public class SetRoleToMemberCommandValidator : AbstractValidator<SetRoleToMemberCommand>
{
    public SetRoleToMemberCommandValidator()
    {
        RuleFor(x => x.Role)
            .NotEmptyAndNotNull()
            .IsInEnum();
    }
}