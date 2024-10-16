using Application.UseCases.ReactionCases.Commands.SendReaction;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.Mapping;

public class ReactionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Reaction, ReactionReadDto>()
            .Map(x => x.Id, x => x.Id)
            .Map(x => x.Emoji, x => x.Emoji)
            .Map(x => x.SenderId, x => x.SenderId)
            .Map(x => x.MessageId, x => x.MessageId);
        
        config.NewConfig<SendReactionCommand, Reaction>()
            .Map(x => x.Id, x => Guid.NewGuid())
            .Map(x => x.Emoji, x => x.Emoji)
            .Map(x => x.SenderId, x => x.UserBlogId)
            .Map(x => x.MessageId, x => x.MessageId);
    }
}