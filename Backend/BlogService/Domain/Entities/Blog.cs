using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class Blog : EntityBase
{
    public string UserId { get; set; }
    
    public string Username { get; set; }
    
    public string? Bio { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string? ImageAttachmentId { get; set; }
    
    public virtual IEnumerable<Subscription> Subscribers { get; set; }
    
    public virtual IEnumerable<Subscription> Subscriptions { get; set; }
    
    public virtual IEnumerable<Post> Posts { get; set; }
    
    public virtual IEnumerable<Comment> SendedComments { get; set; }
    
    public virtual IEnumerable<Like> SendedLikes { get; set; }
}