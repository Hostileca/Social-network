using Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;
using Domain.Entities;
using Mapster;
using SharedResources.Dtos;
using SharedResources.Enums;

namespace Application.Mapping;

public class ChatMemberMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddMemberToChatCommand, ChatMember>()
            .Map(dest => dest.ChatId, src => src.ChatId)
            .Map(dest => dest.BlogId, src => src.BlogToAddId)
            .Map(dest => dest.Role, src => ChatRoles.Member)
            .Map(dest => dest.JoinDate, src => DateTime.UtcNow);

        config.NewConfig<ChatMember, ChatMemberReadDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Blog, src => src.Blog);
        
        config.NewConfig<ChatMember, ChatReadDto>()
            .Map(dest => dest.Id, src => src.ChatId)
            .Map(dest => dest.Name, src => src.Chat.Name)
            .Map(dest => dest.Members, src => src.Chat.Members);
    }
}