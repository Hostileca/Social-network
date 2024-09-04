using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
{
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.SubscribedAt)
            .WithMany(x => x.Subscribers);

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.Subscribtions);
    }
}