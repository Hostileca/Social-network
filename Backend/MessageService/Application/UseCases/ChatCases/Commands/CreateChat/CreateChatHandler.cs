using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IChatNotificationService chatNotificationService)
    : IRequestHandler<CreateChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);
        
        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        var chat = request.Adapt<Chat>();

        var members = new List<ChatMember>
        {
            new ChatMember
            {
                Id = Guid.NewGuid(),
                Blog = userBlog,
                ChatId = chat.Id,
                Role = ChatRoles.Admin,
                JoinDate = DateTime.UtcNow
            }
        };
        
        foreach (var memberId in request.OtherMembers)
        {
            var memberBlog = await blogRepository.GetByIdAsync(memberId, cancellationToken);

            if (memberBlog is null)
            {
                throw new NotFoundException(typeof(Blog).ToString(), memberId.ToString());
            }
            
            members.Add(new ChatMember
            {
                Id = Guid.NewGuid(),
                Blog = memberBlog,
                ChatId = chat.Id,
                Role = ChatRoles.Member,
                JoinDate = DateTime.UtcNow
            });
        }
        
        chat.Members = members;
        
        await chatRepository.AddAsync(chat, cancellationToken);

        await chatRepository.SaveChangesAsync(cancellationToken);

        var chatReadDto = chat.Adapt<ChatReadDto>();

        await chatNotificationService.CreateChatAsync(chatReadDto, cancellationToken);
        
        return chatReadDto;
    }
}