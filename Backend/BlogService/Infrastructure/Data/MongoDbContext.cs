using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MongoDbContext
    : DbContext
{
    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }
    
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Subscription> Subscribers => Set<Subscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}