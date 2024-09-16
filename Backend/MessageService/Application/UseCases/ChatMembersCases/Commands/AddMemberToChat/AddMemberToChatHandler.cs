using Application.Dtos;
using Application.Exceptions;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMembersCases.Commands.AddMemberToChat;

public class AddMemberToChatHandler(
    IChatRepository chatRepository,
    IChatMemberRepository chatMemberRepository,
    IChatMemberNotificationService chatMemberNotificationService)
    : IRequestHandler<AddMemberToChatCommand, ChatMemberReadDto>
{
    public async Task<ChatMemberReadDto> Handle(AddMemberToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }
        
        var userBlog = chat.Members.FirstOrDefault(m => m.BlogId == request.UserBlogId && 
            m.Blog.UserId == request.UserId);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(ChatMember).ToString(), request.UserBlogId.ToString());
        }

        var chatMember = request.Adapt<ChatMember>();
        
        await chatMemberRepository.AddAsync(chatMember, cancellationToken);
        
        await chatMemberRepository.SaveChangesAsync(cancellationToken);
        
        var chatMemberReadDto = chatMember.Adapt<ChatMemberReadDto>();
        
        await chatMemberNotificationService.AddMemberToChatAsync(chatMemberReadDto, request.ChatId, cancellationToken);
        
        return chatMemberReadDto;
    }
}