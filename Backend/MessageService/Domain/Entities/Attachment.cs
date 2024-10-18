﻿namespace Domain.Entities;

public class Attachment : EntityBase
{
    public virtual Message Message { get; set; }
    
    public Guid MessageId { get; set; }
    
    public string Data { get; set; }
    
    public string FileName { get; set; }
    
    public string ContentType { get; set; }
}