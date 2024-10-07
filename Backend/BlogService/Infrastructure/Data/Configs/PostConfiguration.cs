using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Content);

        builder
            .HasIndex(x => x.CreatedAt);
        
        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired();
        
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
        
        builder
            .HasMany(x => x.Likes)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
        
        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
    }
}