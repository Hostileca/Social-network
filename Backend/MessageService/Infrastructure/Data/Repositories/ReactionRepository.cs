using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ReactionRepository(
    AppDbContext context) 
    : RepositoryBase<Reaction>(context), IReactionRepository
{
    public async Task<IEnumerable<Reaction>> GetReactionsByMessageIdAsync(Guid messageId, CancellationToken cancellationToken)
    {
        var spec = new ReactionsByMessageIdSpecification(messageId);

        return await _dbSet
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}