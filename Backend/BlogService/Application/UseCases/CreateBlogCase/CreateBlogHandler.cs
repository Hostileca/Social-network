using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.CreateBlogCase;

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