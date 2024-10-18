using Application.UseCases.BaseQueries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQuery : PagedQuery, IRequest<IEnumerable<ChatReadDto>>
{
    [BindNever]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
}