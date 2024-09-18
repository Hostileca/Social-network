using Application.Dtos;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Exceptions;

namespace Application.UseCases.ChatMembersCases.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatHandler(
    IChatMemberRepository chatMemberRepository,
    IChatMemberNotificationService chatMemberNotificationService)
    : IRequestHandler<RemoveMemberFromChatCommand, ChatMemberReadDto>
{
    public async Task<ChatMemberReadDto> Handle(RemoveMemberFromChatCommand request, CancellationToken cancellationToken)
    {
        var chatMemberToDelete = await chatMemberRepository.GetByIdAsync(request.ChatMemberId, cancellationToken);
        
        if (chatMemberToDelete is null || chatMemberToDelete.ChatId != request.ChatId)
        {
            throw new NotFoundException(typeof(ChatMember).ToString(), request.ChatMemberId.ToString());
        }

        if (chatMemberToDelete.Role == ChatRoles.Admin)
        {
            throw new NoPermissionException("You can not remove admin from chat");
        }

        var userBlogMember = chatMemberToDelete.Chat.Members.FirstOrDefault(m => m.BlogId == request.UserBlogId
            && m.Blog.UserId == request.UserId);

        if (userBlogMember is null)
        {
            throw new NotFoundException(typeof(ChatMember).ToString(), request.UserBlogId.ToString());
        }
        
        switch (userBlogMember.Role)
        {
            case ChatRoles.Member:
                throw new NoPermissionException("You are not an owner of this chat");
            case ChatRoles.Moderator:
                if (chatMemberToDelete.Role != ChatRoles.Member)
                {
                    throw new NoPermissionException("You can not remove moderator or admin from chat");
                }
                chatMemberRepository.Delete(chatMemberToDelete);
                break;
            case ChatRoles.Admin:
                chatMemberRepository.Delete(chatMemberToDelete);
                break;
        }
        
        await chatMemberRepository.SaveChangesAsync(cancellationToken);
        
        var chatMemberReadDto = chatMemberToDelete.Adapt<ChatMemberReadDto>();
        
        await chatMemberNotificationService.RemoveMemberFromChatAsync(chatMemberReadDto, request.ChatId, cancellationToken);
        
        return chatMemberReadDto;
    }
}