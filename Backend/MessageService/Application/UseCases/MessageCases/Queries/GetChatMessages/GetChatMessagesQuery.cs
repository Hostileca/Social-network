using System.Text.Json.Serialization;
using Application.UseCases.BaseQueries;
using Application.UseCases.BaseQueries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.MessageCases.Queries.GetChatMessages;

public class GetChatMessagesQuery : PagedQuery, IRequest<IEnumerable<MessageReadDto>>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromRoute]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}