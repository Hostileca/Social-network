using Application.UseCases.BaseQueries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Queries.GetChatMembers;

public class GetChatMembersQuery : PagedQuery, IRequest<IEnumerable<ChatMemberReadDto>>
{
    [FromRoute]
    public Guid ChatId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
    
    [BindNever]
    public string UserId { get; set; }
}