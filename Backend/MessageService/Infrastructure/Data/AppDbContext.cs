using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatMember> ChatMembers => Set<ChatMember>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Reaction> Reactions => Set<Reaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}