using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.DeleteChat;

public class DeleteChatHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository,
    IChatNotificationService chatNotificationService) 
    : IRequestHandler<DeleteChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        if (!chat.Members.Any(m => m.BlogId == request.UserBlogId && m.Role == ChatRoles.Admin))
        {
            throw new NoPermissionException("You are not an owner of this chat");
        }
        
        chatRepository.Delete(chat);
        
        await chatRepository.SaveChangesAsync(cancellationToken);

        var chatReadDto = chat.Adapt<ChatReadDto>();

        await chatNotificationService.DeleteChatAsync(chatReadDto, cancellationToken);
        
        return chatReadDto;
    }
}