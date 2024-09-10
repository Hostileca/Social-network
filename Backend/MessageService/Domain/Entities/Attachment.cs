namespace Domain.Entities;

public class Attachment : EntityBase
{
    public virtual Message Message { get; set; }
    public Guid MessageId { get; set; }
    public string Path { get; set; }
}