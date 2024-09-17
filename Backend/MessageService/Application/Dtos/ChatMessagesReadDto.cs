namespace Application.Dtos;

public class ChatMessagesReadDto
{
    public IEnumerable<MessageReadDto> Messages { get; set; }
}