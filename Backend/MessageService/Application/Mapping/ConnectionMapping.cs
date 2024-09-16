using Application.UseCases.BlogConnectionCases.Commands.Connect;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public class ConnectionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ConnectCommand, BlogConnection>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.BlogId, src => src.BlogId)
            .Map(dest => dest.ConnectionId, src => src.ConnectionId);
    }
}