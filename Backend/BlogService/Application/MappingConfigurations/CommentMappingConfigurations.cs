using Application.UseCases.CommentCases.Commands.CreateCommentCase;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigurations;

public class CommentMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommentCommand, Comment>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
            .Map(dest => dest.SenderId, src => src.BlogId);
    }
}