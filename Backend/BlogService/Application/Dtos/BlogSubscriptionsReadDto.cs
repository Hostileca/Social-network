namespace Application.Dtos;

public class BlogSubscriptionsReadDto
{
    public IEnumerable<BlogReadDto> Subscriptions { get; set; }
}