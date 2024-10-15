using Application.UseCases.PostCases.Commands.CreatePostCase;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using SharedResources.Dtos;

namespace Application.MappingConfigurations;

public class PostMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Post, PostReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Content, src => src.Content)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.AttachmentsId, 
                src => src.Attachments != null ? src.Attachments.Select(x => x.Id) : new List<string>());

        config.NewConfig<CreatePostCommand, Post>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
            .Map(dest => dest.Content, src => src.Content)
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
            .Map(dest => dest.OwnerId, src => src.BlogId)
            .Map(dest => dest.Attachments,
                src => src.Attachments.Adapt<IEnumerable<Attachment>>(), 
                src => src.Attachments != null);
    }
}