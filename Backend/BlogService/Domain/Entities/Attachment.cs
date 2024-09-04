namespace Domain.Entities;

public class Attachment : EntityBase
{
    public string FilePath { get; set; }
    public virtual Post Post { get; set; }
    public string PostId { get; set; }
}