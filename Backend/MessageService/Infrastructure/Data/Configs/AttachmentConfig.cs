using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.MessageId)
            .IsRequired();

        builder
            .Property(x => x.Data)
            .IsRequired();
        
        builder
            .Property(x => x.ContentType)
            .IsRequired();
        
        builder
            .Property(x => x.FileName)
            .IsRequired();
    }
}