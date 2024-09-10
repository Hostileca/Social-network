namespace Application.Specifications.Interfaces;

public interface ISpecification<TEntity>
{
    Func<TEntity, bool> ToFunction();
}