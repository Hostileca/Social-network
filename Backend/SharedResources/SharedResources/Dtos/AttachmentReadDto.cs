namespace SharedResources.Dtos;

public class AttachmentReadDto
{
    public Guid Id { get; set;}
    
    public string ContentType { get; set; }
    
    public byte[] File { get; set; }
}