namespace Domain.Specifications;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity item);
}