using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.BlogConnectionCases.Commands.Connect;

public class ConnectHandler(
    IBlogRepository blogRepository,
    IBlogConnectionRepository blogConnectionRepository,
    IChatNotificationService chatNotificationService)
    : IRequestHandler<ConnectCommand>
{
    public async Task Handle(ConnectCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.BlogId, 
            request.UserId, cancellationToken);
        
        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId.ToString());
        }

        var connection = request.Adapt<BlogConnection>();
        
        await blogConnectionRepository.AddAsync(connection, cancellationToken);
        
        await blogConnectionRepository.SaveChangesAsync(cancellationToken);
        
        await chatNotificationService.JoinChatsAsync(connection.ConnectionId, userBlog.ChatsMember.Adapt<IEnumerable<ChatReadDto>>(), cancellationToken);
    }
}