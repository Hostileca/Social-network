namespace SharedResources.Dtos;

public class BlogReadDto
{
    public Guid Id {get; set; }
    
    public string UserId { get; set; }
     
    public string Username { get; set; }
     
    public string? BIO { get; set; }
        
    public string? MainImagePath { get; set; }
}