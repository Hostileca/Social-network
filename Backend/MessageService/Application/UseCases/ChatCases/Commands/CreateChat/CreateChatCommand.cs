using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [FromQuery]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public string Name { get; set; }
    
    public List<Guid> OtherMembers { get; set; }
}