namespace SharedResources.Dtos;

public class DelayedMessageReadDto : MessageReadDto
{
    public string JobId { get; set; }
    public DateTimeOffset Delay { get; set; }
}