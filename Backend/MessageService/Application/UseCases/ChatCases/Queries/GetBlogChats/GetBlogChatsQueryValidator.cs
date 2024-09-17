using FluentValidation;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQueryValidator : AbstractValidator<GetBlogChatsQuery>
{
    public GetBlogChatsQueryValidator()
    {
        RuleFor(x => x.UserBlogId)
            .IsGuid();
    }    
}