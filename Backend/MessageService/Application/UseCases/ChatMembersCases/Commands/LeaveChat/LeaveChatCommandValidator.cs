using FluentValidation;

namespace Application.UseCases.ChatMembersCases.Commands.LeaveChat;

public class LeaveChatCommandValidator : AbstractValidator<LeaveChatCommand>
{
    public LeaveChatCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();
    }
}