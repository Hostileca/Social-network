namespace Domain.Entities;

public class Subscriber : EntityBase
{
    public Blog Blog { get; set; }
    public Guid BlogId { get; set; }
    public Blog SubscribedOn { get; set; }
    public Guid SubscribedOnId { get; set; }
}