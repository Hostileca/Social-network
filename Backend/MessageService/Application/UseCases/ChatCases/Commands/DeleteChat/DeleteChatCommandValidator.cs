using FluentValidation;

namespace Application.UseCases.ChatCases.Commands.DeleteChat;

public class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
{
    public DeleteChatCommandValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();
    }
}