namespace SharedResources.MessageBroker.Events;

public class BlogUpdatedEvent
{
    public Guid Id {get; set; }
    
    public string Username { get; set; }
     
    public string? BIO { get; set; }
        
    public string? MainImagePath { get; set; }
}