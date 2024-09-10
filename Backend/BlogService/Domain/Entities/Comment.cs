using MongoDB.Bson;

namespace Domain.Entities;

public class Comment : EntityBase
{
    public string Content { get; set; }
    
    public virtual Blog Sender { get; set; }
    
    public string SenderId { get; set; }
    
    public virtual Post Post { get; set; }
    
    public string PostId { get; set; }
}