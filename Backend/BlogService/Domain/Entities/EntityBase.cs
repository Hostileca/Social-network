using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Domain.Entities;

public class EntityBase
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
}