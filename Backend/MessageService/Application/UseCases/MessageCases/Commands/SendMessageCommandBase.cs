using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.UseCases.MessageCases.Commands;

public abstract class SendMessageCommandBase
{
    [BindNever]
    public string? UserId { get; set; }
        
    [FromRoute]
    public Guid ChatId { get; set; }
        
    [FromQuery]
    public Guid UserBlogId { get; set; }
        
    [FromForm]
    public string Text { get; set; }      
    
    [FromForm]
    public IEnumerable<IFormFile>? Attachments { get; set; }
}