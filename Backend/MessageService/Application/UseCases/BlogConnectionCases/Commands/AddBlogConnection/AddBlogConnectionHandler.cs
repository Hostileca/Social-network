using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Commands.AddBlogConnection;

public class AddBlogConnectionHandler(
    IBlogRepository blogRepository,
    IBlogConnectionRepository blogConnectionRepository)
    : IRequestHandler<AddBlogConnectionCommand, BlogConnectionReadDto>
{
    public async Task<BlogConnectionReadDto> Handle(AddBlogConnectionCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserId(request.BlogId, request.UserId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId.ToString());
        }

        var blogConnection = request.Adapt<BlogConnection>();
        
        await blogConnectionRepository.AddAsync(blogConnection, cancellationToken);
        
        await blogConnectionRepository.SaveChangesAsync(cancellationToken);
        
        return blogConnection.Adapt<BlogConnectionReadDto>();
    }
}