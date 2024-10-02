using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;
using SharedResources.MessageBroker.Events;

namespace Application.MappingConfigurations;

public class BlogMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBlogCommand, Blog>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());

        config.NewConfig<Blog, BlogReadDto>()            
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.BIO, src => src.BIO)
            .Map(dest => dest.MainImagePath, src => src.MainImagePath);

        config.NewConfig<Blog, BlogCreatedEvent>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.UserId, src => src.UserId);
    }
}