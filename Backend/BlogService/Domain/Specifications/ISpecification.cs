namespace Domain.Specifications;

public interface ISpecification<TEntity>
{
    Func<TEntity, bool> ToFunction();
}