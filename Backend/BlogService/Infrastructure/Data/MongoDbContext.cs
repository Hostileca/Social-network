using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data;

public class MongoDbContext
{
    public IMongoCollection<Blog> Blogs { get; set; }

    public MongoDbContext(MongoDbClient mongoDbClient, IConfiguration configuration)
    {
        var database = mongoDbClient.GetDatabase(configuration.GetConnectionString("DbConnection"));
        Blogs = database.GetCollection<Blog>("Blogs");
    }
}