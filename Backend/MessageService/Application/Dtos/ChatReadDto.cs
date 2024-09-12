namespace Application.Dtos;

public class ChatReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ChatMemberReadDto> Members { get; set; }
}