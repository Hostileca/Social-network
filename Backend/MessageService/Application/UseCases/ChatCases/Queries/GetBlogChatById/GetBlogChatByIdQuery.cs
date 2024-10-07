using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChatById;

public class GetBlogChatByIdQuery : IRequest<ChatReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [FromQuery]
    public Guid UserBlogId { get; set; }
    
    [FromRoute]
    public Guid ChatId { get; set; }
}