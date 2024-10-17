using FluentValidation;

namespace Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;

public class AddMemberToChatCommandValidator : AbstractValidator<AddMemberToChatCommand>
{
    public AddMemberToChatCommandValidator()
    {
        RuleFor(x => x.BlogToAddId)
            .IsGuid();
    }
}