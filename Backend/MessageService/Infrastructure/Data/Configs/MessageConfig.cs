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
            .Property(x => x.Text)
            .IsRequired();

        builder
            .HasIndex(x => x.Date)
            .IsDescending()
            .IsClustered();

        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId);

        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.SendedMessages)
            .HasForeignKey(x => x.SenderId);

        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Message);

        builder
            .HasMany(x => x.Reactions)
            .WithOne(x => x.Message);
    }
}