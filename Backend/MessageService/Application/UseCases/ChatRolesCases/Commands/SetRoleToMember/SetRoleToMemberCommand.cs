using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;
using SharedResources.Enums;

namespace Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;

public class SetRoleToMemberCommand : IRequest<ChatMemberReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public Guid MemberId { get; set; }
    
    [FromQuery]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public ChatRoles Role { get; set; }
}