using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MongoDbContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public MongoDbContext(DbContextOptions<MongoDbContext> options)
        : base(options)
    {
    }
}