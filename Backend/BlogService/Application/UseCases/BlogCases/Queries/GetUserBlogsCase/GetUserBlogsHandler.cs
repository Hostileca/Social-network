using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetUserBlogsQuery, UserBlogsDto>
{
    public async Task<UserBlogsDto> Handle(GetUserBlogsQuery request, CancellationToken cancellationToken)
    {
        var blogs = await blogRepository.GetBlogsByUserId(request.UserId, cancellationToken);

        var response = new UserBlogsDto
        {
            UserId = request.UserId,
            Blogs = blogs.Adapt<IEnumerable<BlogReadDto>>()
        };
        
        return response;
    }
}