using Application.Dtos;
using MediatR;

namespace Application.UseCases.SubscriptionCases.SubscribeToBlogCase;
    
public class SubscribeToBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserBlogId { get; set; } 
    public string BlogId { get; set; }
}