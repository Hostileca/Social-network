using Application.Dtos;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogHandler(IBlogRepository repository) 
    : IRequestHandler<CreateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var newBlog = request.Adapt<Blog>();
        
        await repository.AddAsync(newBlog, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);
        
        return newBlog.Adapt<BlogReadDto>();
    }
}