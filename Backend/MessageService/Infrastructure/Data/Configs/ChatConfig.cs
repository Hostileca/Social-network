using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class ChatConfig : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Name);

        builder
            .HasMany(x => x.Members)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);
        
        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);
    }
}