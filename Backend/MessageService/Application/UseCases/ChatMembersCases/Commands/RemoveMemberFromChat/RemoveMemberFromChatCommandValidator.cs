using FluentValidation;

namespace Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommandValidator : AbstractValidator<RemoveMemberFromChatCommand>
{
    public RemoveMemberFromChatCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();
    }
}