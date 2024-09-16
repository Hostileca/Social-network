using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogConnectionCases.Queries.GetBlogConnectionsByBlogId;

public class GetBlogConnectionsByBlogIdCommand : IRequest<IEnumerable<BlogConnectionReadDto>>
{
    public Guid BlogId { get; set; }
}