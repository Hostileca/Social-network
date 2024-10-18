using Application.UseCases.MessageCases.Commands;
using Application.UseCases.MessageCases.Commands.SendDelayedMessage;
using Application.UseCases.MessageCases.Commands.SendMessage;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using SharedResources.Dtos;
using SharedResources.Helpers;

namespace Application.Mapping;

public class MessageMapping : IRegister 
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendMessageCommandBase, Message>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.SenderId, src => src.UserBlogId)
            .Map(dest => dest.ChatId, src => src.ChatId)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Attachments, src => src.Attachments.Adapt<List<Attachment>>().ToList(),
                src => src.Attachments != null);

        config.NewConfig<SendDelayedMessageCommand, DelayedMessageReadDto>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.SenderId, src => src.UserBlogId)
            .Map(dest => dest.ChatId, src => src.ChatId)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Delay, src => src.DateTime);
        
        config.NewConfig<Message, MessageReadDto>()
            .Map(dest =>  dest.Id, src => src.Id)
            .Map(dest => dest.SenderId, src => src.SenderId)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.AttachmentsId, 
                src => src.Attachments != null ? src.Attachments.Select(x => x.Id) : new List<Guid>());

        config.NewConfig<Message, DelayedMessageReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SenderId, src => src.SenderId)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.AttachmentsId,
                src => src.Attachments != null ? src.Attachments.Select(x => x.Id) : new List<Guid>());
    }
}