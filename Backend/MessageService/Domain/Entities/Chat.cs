namespace Domain.Entities;

public class Chat : EntityBase
{
    public string Name { get; set; }
    public virtual IEnumerable<ChatMember> Members { get; set; }
}