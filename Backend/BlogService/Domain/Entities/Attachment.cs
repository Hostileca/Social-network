namespace Domain.Entities;

public class Attachment : EntityBase
{
    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public string FileUrl { get; set; }
}