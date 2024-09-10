namespace Application.Dtos;

public class PostReadDto
{
    public string Id { get; set; }
    
    public string Content { get; set; }
    
    public virtual IEnumerable<string> AttachmentsId { get; set; }
}