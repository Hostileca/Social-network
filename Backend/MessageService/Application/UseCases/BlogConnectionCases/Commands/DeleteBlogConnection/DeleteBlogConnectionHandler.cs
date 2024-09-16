using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.DeleteBlogConnection;

public class DeleteBlogConnectionHandler(
    IBlogConnectionRepository blogConnectionRepository)
    : IRequestHandler<DeleteBlogConnectionCommand, BlogConnectionReadDto>
{
    public async Task<BlogConnectionReadDto> Handle(DeleteBlogConnectionCommand request, CancellationToken cancellationToken)
    {
        var blogConnections = await blogConnectionRepository.GetConnectionsByBlogId(request.BlogId, cancellationToken);

        var connection = blogConnections.FirstOrDefault(x => x.ConnectionId == request.ConnectionId);

        if (connection is null)
        {
            throw new NotFoundException(typeof(BlogConnection).ToString(), request.ConnectionId);
        }
        
        blogConnectionRepository.Delete(connection);
        
        await blogConnectionRepository.SaveChangesAsync(cancellationToken);

        return connection.Adapt<BlogConnectionReadDto>();
    }
}