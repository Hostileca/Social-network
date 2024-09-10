using MongoDB.Bson;

namespace Domain.Entities;

public class Like : EntityBase
{
    public virtual Blog Sender { get; set; }
    
    public string SenderId { get; set; }
    
    public virtual Post Post { get; set; }
    
    public string PostId { get; set; }
}