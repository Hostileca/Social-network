using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatRolesCases.Commands.SetRoleToMember;

public class SetRoleToMemberHandler(
    IChatMemberRepository chatMemberRepository,
    IChatMemberNotificationService chatMemberNotificationService)
    : IRequestHandler<SetRoleToMemberCommand, ChatMemberReadDto>
{
    public async Task<ChatMemberReadDto> Handle(SetRoleToMemberCommand request, CancellationToken cancellationToken)
    {
        var chatMember = await chatMemberRepository.GetByIdAsync(request.UserBlogId, cancellationToken);
        
        if (chatMember is null)
        {
            throw new NotFoundException(typeof(ChatMember).ToString(), request.ChatMemberId.ToString());
        }
        
        var userBlogMember = chatMember.Chat.Members.FirstOrDefault(m => m.BlogId == request.UserBlogId &&
            m.Blog.UserId == request.UserId);

        if (userBlogMember is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        switch (userBlogMember.Role)
        {
            case ChatRoles.Member:
                throw new NoPermissionException("You are not allowed to change role to member");
            case ChatRoles.Moderator:
                if (request.Role == ChatRoles.Member)
                {
                    chatMember.Role = request.Role;
                }
                else
                {
                    throw new NoPermissionException("You are not allowed to change role to moderator or admin");
                }

                break;
            case ChatRoles.Admin:
                if (request.Role == ChatRoles.Admin)
                {
                    userBlogMember.Role = ChatRoles.Moderator;
                }

                chatMember.Role = request.Role;
                break;
        }
        
        await chatMemberRepository.SaveChangesAsync(cancellationToken);

        var chatMemberReadDto = chatMember.Adapt<ChatMemberReadDto>();
        
        await chatMemberNotificationService.ChatMemberUpdateAsync(chatMemberReadDto, chatMember.ChatId, cancellationToken);
        
        return chatMemberReadDto;
    }
}