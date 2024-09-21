using MediatR;
using Newtonsoft.Json;
using SharedResources.Dtos;

namespace Application.UseCases.ChatMembersCases.Commands.LeaveChat;

public class LeaveChatCommand : IRequest<ChatMemberReadDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [JsonIgnore]
    public Guid ChatId { get; set; }
    
    public Guid UserBlogId { get; set; }
}