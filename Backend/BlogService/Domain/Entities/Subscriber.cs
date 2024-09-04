using MongoDB.Bson;

namespace Domain.Entities;

public class Subscriber : EntityBase
{
    public virtual Blog SubscribedAt { get; set; }
    public string SubscribedAtId { get; set; }
    public virtual Blog Blog { get; set; }
    public string BlogId { get; set; }
}