namespace Domain.Entities;

public class Blog : EntityBase
{
    public string UserId { get; set; }
     
    public string Username { get; set; }
     
    public string? BIO { get; set; }
        
    public string? MainImagePath { get; set; }
    
    public virtual IEnumerable<ChatMember> ChatsMember { get; set; }
    
    public virtual IEnumerable<Message> SendedMessages { get; set; }
    
    public virtual IEnumerable<Reaction> SendedReactions { get; set; }
    
    public virtual IEnumerable<BlogConnection> Connections { get; set; }
}