using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class Blog : EntityBase
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string? BIO { get; set; }
    public string? MainImagePath { get; set; }
    public virtual List<Subscriber> Subscribers { get; set; }
    public virtual List<Subscriber> Subscribtions { get; set; }
}