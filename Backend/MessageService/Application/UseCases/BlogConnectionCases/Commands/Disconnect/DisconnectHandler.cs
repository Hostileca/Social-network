using Application.SignalR.Services;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using SharedResources.Exceptions;

namespace Application.UseCases.BlogConnectionCases.Commands.Disconnect;

public class DisconnectHandler(
    IBlogConnectionRepository blogConnectionRepository,
    IBlogRepository blogRepository)
    : IRequestHandler<DisconnectCommand>
{
    public async Task Handle(DisconnectCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await blogRepository.GetBlogByIdAndUserIdAsync(request.BlogId
            , request.UserId, cancellationToken);

        if (userBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId.ToString());
        }

        var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(
            request.BlogId, cancellationToken);
        
        var connection = blogConnections
            .FirstOrDefault(x => x.ConnectionId == request.ConnectionId);
        
        if (connection is null)
        {
            throw new NotFoundException(typeof(BlogConnection).ToString(), request.ConnectionId.ToString());
        }
        
        blogConnectionRepository.Delete(connection);

        await blogConnectionRepository.SaveChangesAsync(cancellationToken);
    }
}