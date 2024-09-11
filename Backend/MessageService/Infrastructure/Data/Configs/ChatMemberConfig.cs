using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class ChatMemberConfig : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Blog)
            .WithMany(x => x.ChatsMember);

        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.Members);

        builder
            .Property(x => x.JoinDate)
            .IsRequired();

        builder
            .Property(x => x.ChatRole)
            .IsRequired();
    }
}