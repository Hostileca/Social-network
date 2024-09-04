using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.SubscribeToBlogCase;

public class SubscribeToBlogCommand : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserBlogId { get; set; } 
    public string BlogId { get; set; }
}