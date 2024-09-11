using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class ReactionConfig : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Emoji);
        
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Reactions)
            .HasForeignKey(x => x.MessageId)
            .IsRequired();
        
        builder
            .HasOne(x => x.BlogSender)
            .WithMany(x => x.SendedReactions)
            .HasForeignKey(x => x.BlogSenderId)
            .IsRequired();
    }
}