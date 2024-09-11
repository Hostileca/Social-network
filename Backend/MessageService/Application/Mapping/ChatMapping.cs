using Application.Dtos;
using Application.UseCases.ChatCases.Commands.CreateChat;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public class ChatMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChatCommand, Chat>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Members, 
                src => src.OtherMembers.Select(id => new ChatMember
            {
                Id = id,
                ChatId = Guid.Empty, 
                JoinDate = DateTime.UtcNow,
                ChatRole = ChatRoles.Member 
            }));
        
        config.NewConfig<Blog, BlogChatsReadDto>()
            .Map(dest => dest.Chats, 
                src => src.ChatsMember.Select(chat => chat));
    }
}