using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.ReactionCases.SendReaction;

public class SendReactionHandler(
    IChatRepository chatRepository,
    IReactionRepository reactionRepository)
    : IRequestHandler<SendReactionCommand, ReactionReadDto>
{
    public async Task<ReactionReadDto> Handle(SendReactionCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetByIdAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        var message = chat.Messages.FirstOrDefault(m => m.Id == request.MessageId);
        
        if (message is null)
        {
            throw new NotFoundException(typeof(Message).ToString(), request.MessageId.ToString());
        }

        var reaction = request.Adapt<Reaction>();
        
        await reactionRepository.AddAsync(reaction, cancellationToken);
        
        return reaction.Adapt<ReactionReadDto>();
    }
}