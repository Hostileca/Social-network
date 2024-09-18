using Application.Dtos;
using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Exceptions;

namespace Application.UseCases.ReactionCases.RemoveReaction;

public class RemoveReactionHandler(
    IBlogRepository blogRepository,
    IReactionRepository reactionRepository,
    IReactionNotificationService reactionNotificationService)
    : IRequestHandler<RemoveReactionCommand, ReactionReadDto>
{
    public async Task<ReactionReadDto> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, 
            request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var reaction = await reactionRepository.GetByIdAsync(request.ReactionId, cancellationToken);

        if (reaction is null || reaction.MessageId != request.MessageId)
        {
            throw new NotFoundException(typeof(Reaction).ToString(), request.ReactionId.ToString());
        }

        if (reaction.Sender != userBlog)
        {
            throw new NoPermissionException("It is not your reaction");
        }

        reactionRepository.Delete(reaction);

        await reactionRepository.SaveChangesAsync(cancellationToken);

        var reactionReadDto = reaction.Adapt<ReactionReadDto>();
        
        await reactionNotificationService.RemoveReaction(reactionReadDto, reaction.Message.ChatId, 
            cancellationToken);

        return reactionReadDto;
    }
}