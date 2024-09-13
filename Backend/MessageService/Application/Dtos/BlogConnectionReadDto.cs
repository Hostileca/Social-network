namespace Application.Dtos;

public class BlogConnectionReadDto
{
    public Guid Id { get; set; }
    
    public Guid BlogId { get; set; }
    
    public string ConnectionId { get; set; }
}