using SharedResources.Dtos;

namespace SharedResources;

public class SubscriberReadDto
{
    public string Id { get; set; }
        
    public BlogReadDto Blog { get; set; }
}