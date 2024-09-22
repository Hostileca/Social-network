using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetUserBlogsQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetUserBlogsQuery request, CancellationToken cancellationToken)
    {
        var blogs = await blogRepository.GetBlogsByUserId(request.UserId, cancellationToken);

        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}