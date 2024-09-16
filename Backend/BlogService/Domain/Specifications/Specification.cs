namespace Domain.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public abstract Func<TEntity, bool> ToFunction();
}