using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogHandler(IBlogRepository repository) 
    : IRequestHandler<DeleteBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var existingBlog = await repository.GetByIdAndUserIdAsync(request.BlogId, request.UserId, cancellationToken);
        
        if (existingBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        repository.Delete(existingBlog);

        await repository.SaveChangesAsync(cancellationToken);
        
        return existingBlog.Adapt<BlogReadDto>();
    }
}