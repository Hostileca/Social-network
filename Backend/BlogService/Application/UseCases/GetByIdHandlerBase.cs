using Application.Exceptions;
using Application.Queries;
using Domain.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.UseCases;

public class GetByIdHandlerBase<TQuery, TEntityReadDto, TEntity> (
    IRepository<TEntity> repository,
    IMapper mapper)
    : IRequestHandler<TQuery, TEntityReadDto> 
    where TQuery : GetByIdQuery<TEntityReadDto>
    where TEntityReadDto : class
    where TEntity : class
{
    public async Task<TEntityReadDto> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(request.Id, cancellationToken);
        
        if(item is null)
            throw new NotFoundException(typeof(TEntityReadDto).ToString());
        
        return mapper.Map<TEntityReadDto>(item);
    }
}