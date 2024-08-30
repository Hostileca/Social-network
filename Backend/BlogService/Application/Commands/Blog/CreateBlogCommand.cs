using Application.Dtos;
using MediatR;

namespace Application.Commands.Blog;

public class CreateBlogCommand : IRequest<BlogReadDto>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
}