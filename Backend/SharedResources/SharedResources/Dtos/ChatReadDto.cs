namespace SharedResources.Dtos;

public class ChatReadDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public int MembersCount { get; set; }
}