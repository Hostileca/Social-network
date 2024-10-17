using Application.UseCases.Base.Queries.Paged;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetAllBlogsCase;

public class GetAllBlogsQuery : PagedQuery, IRequest<IEnumerable<BlogReadDto>>
{
}