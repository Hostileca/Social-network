using Application.Dtos;
using Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigurations;

public class SubscriptionMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubscribeToBlogCommand, Subscription>()
            .Map(dest => dest.SubscribedById, src => src.UserBlogId)
            .Map(dest => dest.SubscribedAtId, src => src.SubscribeAtId)
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());
        
        config.NewConfig<Blog, BlogSubscriptionsReadDto>()
            .Map(dest => dest.Subscriptions, src => src.Subscribtions);
        
        config.NewConfig<Blog, BlogSubscribersReadDto>()
            .Map(dest => dest.SubscribersCount, src => src.Subscribtions.Count());
    }
}