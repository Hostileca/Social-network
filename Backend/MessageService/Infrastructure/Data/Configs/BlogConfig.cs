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
            .Property(x => x.UserId)
            .IsRequired();

        builder
            .Property(x => x.Username)
            .IsRequired();

        builder
            .Property(x => x.Bio);

        builder
            .Property(x => x.ImageAttachmentId);

        builder
            .HasMany(x => x.ChatsMember)
            .WithOne(x => x.Blog);

        builder
            .HasMany(x => x.SendedMessages)
            .WithOne(x => x.Sender)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.SendedReactions)
            .WithOne(x => x.Sender)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Connections)
            .WithOne(x => x.Blog);
    }
}