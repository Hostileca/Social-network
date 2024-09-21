using SharedResources.Enums;

namespace SharedResources.Dtos;

public class ChatMemberReadDto
{
    public Guid Id { get; set; }
    
    public BlogReadDto Blog { get; set; }
    
    public ChatRoles Role { get; set; }
    
    public DateTime JoinDate { get; set; }
}