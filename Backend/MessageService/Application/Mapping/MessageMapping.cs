using Application.UseCases.MessageCases.Commands.SendMessage;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.Mapping;

public class MessageMapping : IRegister 
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendMessageCommand, Message>()
            .Map(dest =>  dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Date, src => DateTime.UtcNow)
            .Map(dest => dest.SenderId, src => src.UserBlogId)
            .Map(dest => dest.ChatId, src => src.ChatId)
            .Map(dest => dest.Text, src => src.Text);
    }
}