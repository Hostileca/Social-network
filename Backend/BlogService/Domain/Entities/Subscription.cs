using MongoDB.Bson;

namespace Domain.Entities;

public class Subscription : EntityBase
{
    public virtual Blog SubscribedAt { get; set; }
    
    public string SubscribedAtId { get; set; }
    
    public virtual Blog SubscribedBy { get; set; }
    
    public string SubscribedById { get; set; }
}