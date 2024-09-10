using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class BlogConfig : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Username)
            .IsRequired();
        
        builder
            .HasMany(x => x.ChatsMember)
            .WithOne(x => x.Blog)
            .HasForeignKey(x => x.BlogId);
        
        builder
            .HasMany(x => x.SendedMessages)
            .WithOne(x => x.SenderBlog)
            .HasForeignKey(x => x.SenderBlogId);
    }
}