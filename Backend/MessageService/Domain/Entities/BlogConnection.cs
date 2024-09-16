namespace Domain.Entities;

public class BlogConnection : EntityBase
{
    public virtual Blog Blog { get; set; }
    public Guid BlogId { get; set; }
    
    public string ConnectionId { get; set; }
}