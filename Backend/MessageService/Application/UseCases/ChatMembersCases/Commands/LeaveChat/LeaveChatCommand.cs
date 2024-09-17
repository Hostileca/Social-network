using Application.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace Application.UseCases.ChatMembersCases.Commands.LeaveChat;

public class LeaveChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}