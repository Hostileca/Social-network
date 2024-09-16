namespace Application.Dtos;

public class UserBlogsDto
{
    public string UserId { get; set; }
    
    public IEnumerable<BlogReadDto> Blogs { get; set; }
}