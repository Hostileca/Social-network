using Application.UseCases.ChatCases.Commands.CreateChat;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.Mapping;

public class ChatMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChatCommand, Chat>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Name, src => src.Name);
        
        config.NewConfig<Chat, ChatReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Members, src => src.Members.Select(member => member.Adapt<ChatMemberReadDto>()));
    }
}