using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Queries.GetAllBlogsCase;

public class GetAllBlogsQuery : IRequest<IEnumerable<BlogReadDto>>
{
}