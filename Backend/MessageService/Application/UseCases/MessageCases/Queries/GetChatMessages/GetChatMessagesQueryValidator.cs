using FluentValidation;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesQueryValidator : AbstractValidator<GetChatMessagesQuery>
{
    public GetChatMessagesQueryValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();
    }
}