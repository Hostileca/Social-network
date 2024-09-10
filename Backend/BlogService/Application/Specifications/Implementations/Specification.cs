using Application.Specifications.Interfaces;

namespace Application.Specifications.Implementations;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public abstract Func<TEntity, bool> ToFunction();
}