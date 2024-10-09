using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;

public class AddMemberToChatCommand : IRequest<ChatMemberReadDto>
{
    [BindNever]
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [FromQuery]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public Guid BlogToAddId { get; set; } 
}