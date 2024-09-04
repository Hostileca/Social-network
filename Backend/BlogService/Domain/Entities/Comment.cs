using MongoDB.Bson;

namespace Domain.Entities;

public class Comment : EntityBase
{
    public string Content { get; set; }
    //public ObjectId SenderId { get; set; }
}