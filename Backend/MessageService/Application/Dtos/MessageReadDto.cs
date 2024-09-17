namespace Application.Dtos;

public class MessageReadDto
{
    public Guid Id { get; set; }
    
    public Guid SenderId { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Text { get; set; }
    
    public IEnumerable<ReactionReadDto> Reactions { get; set; }
}