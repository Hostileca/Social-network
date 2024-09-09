namespace Application.Specifications;

public interface ISpecification<TEntity>
{
    Func<TEntity, bool> ToFunction();
}