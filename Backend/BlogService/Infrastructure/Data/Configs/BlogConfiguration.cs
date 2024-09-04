using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
            .IsRequired();

        builder.Property(x => x.BIO);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MainImagePath);
        
        builder.HasMany(x => x.Subscribers)
            .WithOne(x => x.SubscribedAt)
            .HasForeignKey(x => x.SubscribedAtId);
        
        builder.HasMany(x => x.Subscribtions)
            .WithOne(x => x.Blog)
            .HasForeignKey(x => x.BlogId);
    }
}