using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Queries.GetBlogByIdCase;

public class GetBlogByIdQuery: IRequest<BlogReadDto>
{
    public string? BlogId { get; set; }
}