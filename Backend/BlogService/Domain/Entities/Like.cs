namespace Domain.Entities;

public class Like : EntityBase
{
    public Blog Sender { get; set; }
    public Guid SenderId { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
}