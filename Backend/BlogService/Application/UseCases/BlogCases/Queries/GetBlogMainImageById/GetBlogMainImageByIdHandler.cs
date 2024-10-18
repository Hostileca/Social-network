using Domain.Entities;
using Domain.Repositories;
using MediatR;
using SharedResources.Exceptions;

namespace Application.UseCases.BlogCases.Queries.GetBlogMainImageById;

public class GetBlogMainImageByIdHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogMainImageByIdQuery, byte[]>
{
    public async Task<byte[]> Handle(GetBlogMainImageByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }

        if (blog.MainImage is null)
        {
            throw new NotFoundException("image", request.BlogId);
        }
        return Convert.FromBase64String(blog.MainImage);
    }
}