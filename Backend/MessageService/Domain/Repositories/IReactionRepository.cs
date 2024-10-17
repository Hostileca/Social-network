using Domain.Entities;

namespace Domain.Repositories;

public interface IReactionRepository : IRepository<Reaction>
{
    Task<IEnumerable<Reaction>> GetReactionsByMessageIdAsync(Guid messageId, CancellationToken cancellationToken);
}