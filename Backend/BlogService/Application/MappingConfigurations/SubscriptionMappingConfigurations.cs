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
            .Map(dest => dest.Id, src => src.SubscribedAt.Id)
            .Map(dest => dest.Username, src => src.SubscribedAt.Username)
            .Map(dest => dest.BIO, src => src.SubscribedAt.BIO)
            .Map(dest => dest.MainImagePath, src => src.SubscribedAt.MainImagePath);
        
        config.NewConfig<Subscription, SubscriberReadDto>()
            .Map(dest => dest.Id, src => src.SubscribedBy.Id)
            .Map(dest => dest.Username, src => src.SubscribedBy.Username)
            .Map(dest => dest.BIO, src => src.SubscribedBy.BIO)
            .Map(dest => dest.MainImagePath, src => src.SubscribedBy.MainImagePath);
    }
}