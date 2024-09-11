using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatCases.Commands.CreateChat;

public class CreateChatHandler(
    IBlogRepository blogRepository,
    IChatRepository chatRepository)
    : IRequestHandler<CreateChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (userBlog.UserId != request.UserId)
        {
            throw new NoPermissionException("It is not your blog");
        }

        var chat = request.Adapt<Chat>();

        var members = chat.Members.ToList();
        members.Add(new ChatMember
        {
            Id = Guid.NewGuid(),
            BlogId = request.BlogId,
            ChatId = chat.Id,
            ChatRole = ChatRoles.Admin,
            JoinDate = DateTime.UtcNow
        });
        chat.Members = members;
        
        await chatRepository.AddAsync(chat, cancellationToken);

        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}