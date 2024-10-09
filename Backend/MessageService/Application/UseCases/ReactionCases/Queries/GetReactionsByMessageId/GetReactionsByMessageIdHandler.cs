using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.ReactionCases.Queries.GetReactionsByMessageId;

public class GetReactionsByMessageIdHandler(
    IBlogRepository blogRepository,
    IMessageRepository messageRepository,
    IReactionRepository reactionRepository)
    : IRequestHandler<GetReactionsByMessageIdQuery, IEnumerable<ReactionReadDto>>
{
    public async Task<IEnumerable<ReactionReadDto>> Handle(GetReactionsByMessageIdQuery request, CancellationToken cancellationToken)
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
        
        if(message.Chat.Members.All(x => x.BlogId != request.UserBlogId))
        {
            throw new NotFoundException(typeof(Message).ToString(), request.MessageId.ToString());
        }
        
        var reactions = await reactionRepository.GetReactionsByMessageIdAsync(request.MessageId, cancellationToken);

        return reactions.Adapt<IEnumerable<ReactionReadDto>>();
    }
}