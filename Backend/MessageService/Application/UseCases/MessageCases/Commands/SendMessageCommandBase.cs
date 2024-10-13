using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.UseCases.MessageCases.Commands;

public abstract class SendMessageCommandBase
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
        
    [BindNever]
    [JsonIgnore]
    public Guid ChatId { get; set; }
        
    [BindNever]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
        
    public string Text { get; set; }      
    
    public IEnumerable<IFormFile>? Attachments { get; set; }
}