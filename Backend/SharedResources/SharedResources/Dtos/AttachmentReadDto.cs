namespace SharedResources.Dtos;

public class AttachmentReadDto
{
    public string ContentType { get; set; }
    
    public byte[] File { get; set; }
}