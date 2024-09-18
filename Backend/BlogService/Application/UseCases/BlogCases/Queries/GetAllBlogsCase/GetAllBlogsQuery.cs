using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetAllBlogsCase;

public class GetAllBlogsQuery : IRequest<IEnumerable<BlogReadDto>>
{
}