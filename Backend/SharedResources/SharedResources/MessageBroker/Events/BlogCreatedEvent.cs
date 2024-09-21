namespace SharedResources.MessageBroker.Events;

public class BlogCreatedEvent
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string UserId { get; set; }
}