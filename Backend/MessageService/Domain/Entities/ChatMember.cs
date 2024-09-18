using SharedResources.Enums;

namespace Domain.Entities;

public class ChatMember : EntityBase
{
    public virtual Blog Blog { get; set; }
    
    public Guid BlogId { get; set; }
    
    public virtual Chat Chat { get; set; }
    
    public Guid ChatId { get; set; }
    
    public DateTime JoinDate { get; set; }
    
    public ChatRoles Role { get; set; }
}