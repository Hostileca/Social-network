namespace SharedResources.Dtos;

public class ChatMessagesReadDto
{
    public IEnumerable<MessageReadDto> Messages { get; set; }
}