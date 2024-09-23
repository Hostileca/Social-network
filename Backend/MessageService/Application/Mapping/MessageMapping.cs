using Application.UseCases.MessageCases.Commands;
using Application.UseCases.MessageCases.Commands.SendDelayedMessage;
using Application.UseCases.MessageCases.Commands.SendMessage;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.Mapping;

public class MessageMapping : IRegister 
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendMessageCommandBase, Message>()
            .Map(dest =>  dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Date, src => DateTime.UtcNow)
            .Map(dest => dest.SenderId, src => src.UserBlogId)
            .Map(dest => dest.ChatId, src => src.ChatId)
            .Map(dest => dest.Text, src => src.Text);
        
        config.NewConfig<SendDelayedMessageCommand, DelayedMessageReadDto>()
            .Map(dest =>  dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Date, src => DateTime.UtcNow)
            .Map(dest => dest.SenderId, src => src.UserBlogId)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Delay, src => src.Delay);
    }
}