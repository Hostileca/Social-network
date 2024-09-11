using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Text);
        
        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .IsRequired();
        
        builder
            .HasOne(x => x.SenderBlog)
            .WithMany(x => x.SendedMessages)
            .HasForeignKey(x => x.SenderBlogId)
            .IsRequired();

        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Message);

        builder
            .HasMany(x => x.Reactions)
            .WithOne(x => x.Message);
    }
}