using Application.Specifications.Interfaces;

namespace Application.Specifications.Simple;

public class AndSpecification<TEntity>(ISpecification<TEntity> left, ISpecification<TEntity> right)
    : Specification<TEntity>
{
    public override Func<TEntity, bool> ToFunction()
    {
        var leftPredicate = left.ToFunction();
        var rightPredicate = right.ToFunction();
        return entity => leftPredicate(entity) && rightPredicate(entity);
    }
}