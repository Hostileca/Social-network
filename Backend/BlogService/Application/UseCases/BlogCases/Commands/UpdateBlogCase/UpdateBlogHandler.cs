using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<UpdateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var existingBlog = await blogRepository.GetByIdAndUserIdAsync(request.Id, request.UserId ,cancellationToken);
        
        if (existingBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.Id);
        }
        
        existingBlog = request.Adapt<Blog>();

        await blogRepository.SaveChangesAsync(cancellationToken);
        
        return existingBlog.Adapt<BlogReadDto>();
    }
}