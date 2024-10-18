using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.FileName)
            .IsRequired();
        
        builder
            .Property(x => x.ContentType)
            .IsRequired();
        
        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.PostId);

        builder
            .Property(x => x.Data)
            .IsRequired();
    }
}