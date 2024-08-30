using Domain.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.UseCases;

public class CreateHandlerBase<TCreateCommand, TEntityReadDto, TEntity> (
    IRepository<TEntity> repository,
    IMapper mapper)
    : IRequestHandler<TCreateCommand, TEntityReadDto> 
    where TCreateCommand : IRequest<TEntityReadDto>
    where TEntityReadDto : class
    where TEntity : class
{
    public async Task<TEntityReadDto> Handle(TCreateCommand request, CancellationToken cancellationToken)
    {
        var newItem = mapper.Map<TEntity>(request);
        await repository.AddAsync(newItem, cancellationToken);
        return mapper.Map<TEntityReadDto>(newItem);
    }
}