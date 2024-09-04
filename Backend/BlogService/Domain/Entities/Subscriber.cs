using MongoDB.Bson;

namespace Domain.Entities;

public class Subscriber : EntityBase
{
    public virtual Blog SubscribedAt { get; set; }
    public ObjectId SubscribedAtId { get; set; }
    public virtual Blog Blog { get; set; }
    public ObjectId BlogId { get; set; }
}