using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.GetAllBlogsCase;

public class GetAllBlogsQuery : IRequest<IEnumerable<BlogReadDto>>
{
}