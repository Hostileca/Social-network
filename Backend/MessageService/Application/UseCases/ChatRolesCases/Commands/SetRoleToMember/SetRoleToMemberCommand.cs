using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Enums;

namespace Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;

public class SetRoleToMemberCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatMemberId { get; set; }
    
    public Guid UserBlogId { get; set; }
    
    public ChatRoles Role { get; set; }
}