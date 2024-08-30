namespace Domain.Entities;

public class Blog : EntityBase
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string BIO { get; set; }
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<Subscriber> Subscribers { get; set; }
}