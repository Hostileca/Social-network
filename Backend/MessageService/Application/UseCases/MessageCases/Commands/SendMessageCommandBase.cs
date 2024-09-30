using System.Text.Json.Serialization;

namespace Application.UseCases.MessageCases.Commands;

public abstract class SendMessageCommandBase
{
    [JsonIgnore]
    public string? UserId { get; set; }
        
    [JsonIgnore]
    public Guid ChatId { get; set; }
        
    public Guid UserBlogId { get; set; }
        
    public string Text { get; set; }      
}