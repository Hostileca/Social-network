using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
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
            .Property(x => x.BIO);

        builder
            .Property(x => x.MainImagePath);
        
        builder
            .HasMany(x => x.Subscribers)
            .WithOne(x => x.SubscribedAt)
            .HasForeignKey(x => x.SubscribedAtId);
        
        builder
            .HasMany(x => x.Subscribtions)
            .WithOne(x => x.SubscribedBy)
            .HasForeignKey(x => x.SubscribedById);
        
        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);
        
        builder
            .HasMany(x => x.SendedComments)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);
        
        builder
            .HasMany(x => x.SendedLikes)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);
    }
}