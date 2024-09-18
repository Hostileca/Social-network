using Application.UseCases.SubscriptionCases.Commands.SubscribeToBlogCase;
using Domain.Entities;
using Mapster;
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

        config.NewConfig<Blog, BlogSubscriptionsReadDto>()
            .Map(dest => dest.Subscriptions, 
                src => src.Subscriptions.Select(sub => new BlogReadDto
                {
                    Id = sub.SubscribedAtId,
                    Username = sub.SubscribedAt.Username,
                }).ToList());
        
        config.NewConfig<Blog, BlogSubscribersReadDto>()
            .Map(dest => dest.Subscribers, 
                src => src.Subscribers.Select(sub => new BlogReadDto
                {
                    Id = sub.SubscribedById,
                    Username = sub.SubscribedBy.Username,
                }).ToList());
    }
}