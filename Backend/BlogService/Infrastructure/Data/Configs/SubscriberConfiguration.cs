using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class SubscriberConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.SubscribedAt)
            .WithMany(x => x.Subscribers);

        builder.HasOne(x => x.SubscribedBy)
            .WithMany(x => x.Subscriptions);
    }
}