using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Attachment : EntityBase
{
    public string Data { get; set; }
    
    public string ContentType { get; set; }
    
    public string FileName { get; set; }
    
    public virtual Post Post { get; set; }
    
    public string PostId { get; set; }
}