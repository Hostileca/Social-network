using System.Linq.Expressions;

namespace SharedResources.Specifications;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
    Expression<Func<TEntity, bool>> ToExpression();
}