﻿namespace Domain.Entities;

public class Comment : EntityBase
{
    public string Content { get; set; }
    public Guid SenderId { get; set; }
}