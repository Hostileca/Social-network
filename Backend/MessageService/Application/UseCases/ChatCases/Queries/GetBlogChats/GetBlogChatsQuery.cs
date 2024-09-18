using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQuery : IRequest<BlogChatsReadDto>
{
    public string? UserId { get; set; }
    public Guid UserBlogId { get; set; }
}