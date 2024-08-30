namespace Domain.Entities;

public class Comment : EntityBase
{
    public string Content { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public Blog Sender { get; set; }
    public Guid SenderId { get; set; }
}