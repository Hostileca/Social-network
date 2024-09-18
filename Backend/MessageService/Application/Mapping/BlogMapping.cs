using Domain.Entities;
using Mapster;
using SharedResources.MessageBroker.Events;

namespace Application.Mapping;

public class BlogMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BlogCreatedEvent, Blog>();
    }
}