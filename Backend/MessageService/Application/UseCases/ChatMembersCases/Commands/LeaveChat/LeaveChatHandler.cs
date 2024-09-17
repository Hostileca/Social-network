using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMembersCases.Commands.LeaveChat;

public class LeaveChatHandler(
    IBlogRepository blogRepository,
    IChatMemberRepository chatMemberRepository,
    IChatMemberNotificationService chatMemberNotificationService)
    : IRequestHandler<LeaveChatCommand, ChatMemberReadDto>
{
    public async Task<ChatMemberReadDto> Handle(LeaveChatCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository
            .GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chatMember = userBlog.ChatsMember.FirstOrDefault(cm => cm.ChatId == request.ChatId);
        
        if (chatMember is null)
        {
            throw new NotFoundException(typeof(ChatMember).ToString(), request.ChatId.ToString());
        }

        if (chatMember.Role == ChatRoles.Admin)
        {
            throw new NoPermissionException("Admin can not leave chat");
        }
        
        chatMemberRepository.Delete(chatMember);

        await chatMemberRepository.SaveChangesAsync(cancellationToken);
        
        var chatMemberReadDto = chatMember.Adapt<ChatMemberReadDto>();

        await chatMemberNotificationService.ChatMemberLeaveAsync(chatMemberReadDto, chatMember.ChatId,
            cancellationToken);

        return chatMemberReadDto;
    }
}