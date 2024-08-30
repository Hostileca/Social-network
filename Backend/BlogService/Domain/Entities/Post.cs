namespace Domain.Entities;

public class Post : EntityBase
{
    public string Content { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<Attachment> Attachments { get; set; }
}