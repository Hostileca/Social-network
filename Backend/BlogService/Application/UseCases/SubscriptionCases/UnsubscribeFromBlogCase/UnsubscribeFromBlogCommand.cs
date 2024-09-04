using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.UnsubscribeFromBlogCase;

public class UnsubscribeFromBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserBlogId { get; set; }
    public string BlogId { get; set; }
}