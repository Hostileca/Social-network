using Application.Dtos;
using Application.UseCases.PostCases.Commands.CreatePostCase;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.MappingConfigurations;

public class PostMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Post, PostReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AttachmentsId, src => src.Attachments.Select(x => x.Id));

        config.NewConfig<CreatePostCommand, Post>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
            .Map(dest => dest.OwnerId, src => src.BlogId)
            .Ignore(dest => dest.Attachments);
    }
}