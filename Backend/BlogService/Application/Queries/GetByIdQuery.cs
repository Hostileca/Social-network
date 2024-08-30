using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetByIdQuery<TEntityReadDto> : IRequest<TEntityReadDto>
{
    public Guid Id { get; set; }
}