using MongoDB.Driver;

namespace Infrastructure.Data;

public class MongoDbClient(string MongoDbConnectionString) : MongoClient(MongoDbConnectionString)
{
    
}