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
    }
}