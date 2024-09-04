using Application.Dtos;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogHandler(IBlogRepository repository) 
    : IRequestHandler<DeleteBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var newBlog = request.Adapt<Blog>();
        
        await repository.AddAsync(newBlog, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);
        
        return newBlog.Adapt<BlogReadDto>();
    }
}