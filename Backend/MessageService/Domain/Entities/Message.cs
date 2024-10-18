namespace Domain.Entities;

public class Message : EntityBase
{
    public string Text { get; set; }
    
    public DateTime Date { get; set; }
    
    public virtual Blog? Sender { get; set; }
    
    public Guid? SenderId { get; set; }
    
    public virtual Chat Chat { get; set; }
    
    public Guid ChatId { get; set; }
    
    public virtual IEnumerable<Reaction> Reactions { get; set; }
    
    public virtual List<Attachment> Attachments { get; set; }
}