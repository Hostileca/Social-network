using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ReactionCases.Commands.SendReaction;

public class SendReactionHandler(
    IBlogRepository blogRepository,
    IMessageRepository messageRepository,
    IReactionRepository reactionRepository,
    IReactionNotificationService reactionNotificationService)
    : IRequestHandler<SendReactionCommand, ReactionReadDto>
{
    public async Task<ReactionReadDto> Handle(SendReactionCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);
        
        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var message = await messageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        
        if (message is null)
        {
            throw new NotFoundException(typeof(Message).ToString(), request.MessageId.ToString());
        }

        if (message.Chat.Members.All(m => m.Blog != userBlog))
        {
            throw new NoPermissionException("User is not a member of the chat");
        }

        var existingReaction = message.Reactions.FirstOrDefault(r => r.SenderId == request.UserBlogId);

        if (existingReaction is not null)
        {
            reactionRepository.Delete(existingReaction);
        }

        var reaction = request.Adapt<Reaction>();
        
        await reactionRepository.AddAsync(reaction, cancellationToken);
        
        await reactionRepository.SaveChangesAsync(cancellationToken);

        var reactionReadDto = reaction.Adapt<ReactionReadDto>();

        await reactionNotificationService.SendReaction(reactionReadDto, reaction.Message.ChatId,
            cancellationToken);
        
        return reactionReadDto;
    }
}