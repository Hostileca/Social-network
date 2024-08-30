using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class Blog : EntityBase
{
    public Guid UserId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string BIO { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string MainImagePath { get; set; }
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Subscriber> Subscribers { get; set; }
}