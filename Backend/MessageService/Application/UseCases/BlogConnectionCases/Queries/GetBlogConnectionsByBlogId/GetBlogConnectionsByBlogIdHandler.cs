using Application.Dtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Queries.GetBlogConnectionsByBlogId;

public class GetBlogConnectionsByBlogIdHandler(
    IBlogConnectionRepository blogConnectionRepository)
    : IRequestHandler<GetBlogConnectionsByBlogIdCommand, IEnumerable<BlogConnectionReadDto>>
{
    public async Task<IEnumerable<BlogConnectionReadDto>> Handle(GetBlogConnectionsByBlogIdCommand request, CancellationToken cancellationToken)
    {
        var blogConnections = await blogConnectionRepository
            .GetConnectionsByBlogId(request.BlogId, cancellationToken);
        
        return blogConnections.Adapt<IEnumerable<BlogConnectionReadDto>>();
    }
}