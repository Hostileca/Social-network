using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionHandler(
    IBlogRepository blogRepository,
    IReactionRepository reactionRepository)
    : IRequestHandler<SendReactionCommand, ReactionReadDto>
{
    public async Task<ReactionReadDto> Handle(SendReactionCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserId(request.UserBlogId, request.UserId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }
        
        var chatMember = blog.ChatsMember.FirstOrDefault(cm => cm.ChatId == request.ChatId);

        if (chatMember is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        var message = chatMember.Chat.Messages.FirstOrDefault(m => m.Id == request.MessageId);
        
        if (message is null)
        {
            throw new NotFoundException(typeof(Message).ToString(), request.MessageId.ToString());
        }

        var reaction = request.Adapt<Reaction>();
        
        await reactionRepository.AddAsync(reaction, cancellationToken);
        
        await reactionRepository.SaveChangesAsync(cancellationToken);
        
        return reaction.Adapt<ReactionReadDto>();
    }
}