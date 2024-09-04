using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.SendedLikes);

        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Likes);
    }
}