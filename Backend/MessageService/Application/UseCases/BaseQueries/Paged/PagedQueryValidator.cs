using FluentValidation;

namespace Application.UseCases.BaseQueries.Paged;

public class PagedQueryValidator : AbstractValidator<PagedQuery>
{
    public PagedQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .Must(x => x > 0)
            .WithMessage("Page number must be greater than zero");
        
        RuleFor(x => x.PageSize)
            .Must(x => x > 0)
            .WithMessage("Page size must be greater than zero");
    }
}