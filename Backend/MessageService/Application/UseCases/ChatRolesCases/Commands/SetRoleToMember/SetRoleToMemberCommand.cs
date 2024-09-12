using System.Text.Json.Serialization;
using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;

public class SetRoleToMemberCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    [JsonIgnore]
    public Guid ChatMemberId { get; set; }
    
    public ChatRoles Role { get; set; }
}