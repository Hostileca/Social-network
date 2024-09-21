using Application.UseCases.LikeCases.Commands.AddLikeToPostCase;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.MappingConfigurations;

public class LikeMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddLikeToPostCommand, Like>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
            .Map(dest => dest.PostId, src => src.PostId)
            .Map(dest => dest.SenderId, src => src.BlogId);
        
        config.NewConfig<Post, PostLikesReadDto>()
            .Map(dest => dest.LikesCount, src => src.Likes.Count());
        
        config.NewConfig<Blog, BlogLikesReadDto>()
            .Map(dest => dest.Posts, src => src.SendedLikes.Select(x => x.Post));
    }
}