using Application.Dtos;
using Application.UseCases.BlogConnectionCases.Commands.AddBlogConnection;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public class BlogConnectionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddBlogConnectionCommand, BlogConnection>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.ConnectionId, src => src.ConnectionId)
            .Map(dest => dest.BlogId, src => src.BlogId);

        config.NewConfig<BlogConnection, BlogConnectionReadDto>();

    }
}