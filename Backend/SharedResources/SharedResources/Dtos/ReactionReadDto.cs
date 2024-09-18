namespace SharedResources.Dtos;

public class ReactionReadDto
{
    public Guid Id { get; set; }
    
    public string Emoji { get; set; }
    
    public Guid MessageId { get; set; }
}