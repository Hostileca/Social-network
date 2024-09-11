namespace Domain.Entities;

public class Reaction : EntityBase
{
    public string Emoji { get; set; }
    
    public virtual Message Message { get; set; }
    
    public Guid MessageId { get; set; }
    
    public virtual Blog BlogSender { get; set; }
    
    public Guid BlogSenderId { get; set; }
}