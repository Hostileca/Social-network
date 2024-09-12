using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.DeleteChat;

public class DeleteChatHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository) 
    : IRequestHandler<DeleteChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId.ToString());
        }

        if (userBlog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }
        
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        if (!chat.Members.Any(m => m.BlogId == request.BlogId && m.Role == ChatRoles.Admin))
        {
            throw new NoPermissionException("You are not an owner of this chat");
        }
        
        chatRepository.Delete(chat);
        
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}