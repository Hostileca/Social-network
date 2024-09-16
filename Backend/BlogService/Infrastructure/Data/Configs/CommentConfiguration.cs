using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Content)
            .IsRequired();

        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.SendedComments)
            .IsRequired();
        
        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Comments)
            .IsRequired();
    }
}