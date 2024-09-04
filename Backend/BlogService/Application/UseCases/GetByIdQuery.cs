using Application.Dtos;
using MediatR;

namespace Application.UseCases;

public class GetByIdQuery<TEntityReadDto> : IRequest<TEntityReadDto>
{
    public Guid Id { get; set; }
}