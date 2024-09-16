using Application.Dtos;
using Domain.Repositories;
using Infrastructure.Specifications;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetUserBlogsQuery, UserBlogsDto>
{
    public async Task<UserBlogsDto> Handle(GetUserBlogsQuery request, CancellationToken cancellationToken)
    {
        var specification = new UserBlogsSpecification(request.UserId);
        var blogs = await blogRepository.FindAsync(specification, cancellationToken);

        var response = new UserBlogsDto
        {
            UserId = request.UserId,
            Blogs = blogs.Adapt<IEnumerable<BlogReadDto>>()
        };
        
        return response;
    }
}