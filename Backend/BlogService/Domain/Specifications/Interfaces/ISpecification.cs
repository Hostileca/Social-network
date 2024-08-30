namespace Domain.Specifications.Interfaces;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity item);
}