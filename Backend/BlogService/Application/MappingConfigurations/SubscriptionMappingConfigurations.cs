using Application.UseCases.SubscriptionCases.SubscribeToBlogCase;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigurations;

public class SubscriptionMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubscribeToBlogCommand, Subscriber>()
            .Map(dest => dest.BlogId, src => src.UserBlogId)
            .Map(dest => dest.SubscribedAtId, src => src.BlogId);
    }
}