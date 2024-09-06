using System.Collections;

namespace Domain.Entities;

public class Post : EntityBase
{
    public string Content { get; set; }
    public virtual Blog Owner { get; set; }
    public string OwnerId { get; set; }
    public virtual IEnumerable<Comment> Comments { get; set; }
    public virtual IEnumerable<Like> Likes { get; set; }
    public virtual IEnumerable<Attachment> Attachments { get; set; }
}