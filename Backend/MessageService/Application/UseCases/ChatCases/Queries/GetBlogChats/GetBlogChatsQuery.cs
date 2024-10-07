using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQuery : IRequest<IEnumerable<ChatReadDto>>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
}