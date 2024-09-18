using FluentValidation;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
{
    public const int MaxNameLength = 50;
    
    public CreateChatCommandValidator()
    {
        RuleFor(x => x.UserBlogId).IsGuid();

        RuleFor(x => x.Name)
            .NotEmptyAndNotNull()
            .MaximumLength(MaxNameLength);
        
        RuleFor(x => x.OtherMembers)
            .NotEmptyAndNotNull()
            .Must(x => x.Count > 0);
    }    
}