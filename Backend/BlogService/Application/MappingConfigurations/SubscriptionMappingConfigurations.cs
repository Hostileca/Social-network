using Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
using Domain.Entities;
using Mapster;
using SharedResources;
using SharedResources.Dtos;

namespace Application.MappingConfigurations;

public class SubscriptionMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubscribeToBlogCommand, Subscription>()
            .Map(dest => dest.SubscribedById, src => src.UserBlogId)
            .Map(dest => dest.SubscribedAtId, src => src.SubscribeAtId)
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());

        config.NewConfig<Subscription, SubscriptionReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Blog, src => src.SubscribedAt);

        config.NewConfig<Subscription, SubscriberReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Blog, src => src.SubscribedBy);
    }
}