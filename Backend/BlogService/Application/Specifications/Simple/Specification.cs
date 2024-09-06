using Application.Specifications.Interfaces;

namespace Application.Specifications.Simple;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public abstract Func<TEntity, bool> ToFunction();
    
    public Specification<TEntity> And(Specification<TEntity> specification)
    {
        return new AndSpecification<TEntity>(this, specification);
    }
}