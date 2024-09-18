using System.Linq.Expressions;

namespace Domain.Specifications;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
    Expression<Func<TEntity, bool>> ToExpression();
}