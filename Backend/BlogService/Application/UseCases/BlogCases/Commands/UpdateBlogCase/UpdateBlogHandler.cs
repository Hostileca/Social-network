using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<UpdateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var existingBlog = await blogRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        existingBlog = request.Adapt<Blog>();

        await blogRepository.SaveChangesAsync(cancellationToken);
        
        return existingBlog.Adapt<BlogReadDto>();
    }
}