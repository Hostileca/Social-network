using Application.Dtos;
using MediatR;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQuery : IRequest<BlogChatsReadDto>
{
    public string UserId { get; set; }
    public Guid BlogId { get; set; }
}