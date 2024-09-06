using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Attachment : EntityBase
{
    public string FilePath { get; set; }
    public virtual Post Post { get; set; }
    public string PostId { get; set; }
    public IFormFile File { get; set; }
}