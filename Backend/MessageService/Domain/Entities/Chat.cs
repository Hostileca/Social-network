namespace Domain.Entities;

public class Chat : EntityBase
{
    public string Name { get; set; }
    
    public virtual IEnumerable<ChatMember> Members { get; set; }
    
    public virtual IEnumerable<Message> Messages { get; set; }
}