namespace SharedResources.Dtos;

public class BlogReadDto
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
     
    public string? Bio { get; set; }
    
    public DateTime DateOfBirth { get; set; }
        
    public string? ImageAttachmentId { get; set; }
    
    public int PostsCount { get; set; }
    
    public int SubscribersCount { get; set; }
    
    public int SubscriptionsCount { get; set; }
}