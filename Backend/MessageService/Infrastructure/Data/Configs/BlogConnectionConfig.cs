using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class BlogConnectionConfig : IEntityTypeConfiguration<BlogConnection>
{
    public void Configure(EntityTypeBuilder<BlogConnection> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Blog)
            .WithMany(x => x.Connections)
            .HasForeignKey(x => x.BlogId);

        builder
            .Property(x => x.ConnectionId)
            .IsRequired();
    }
}