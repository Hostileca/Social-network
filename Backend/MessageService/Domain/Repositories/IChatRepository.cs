using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IChatRepository : IRepository<Chat>
{
    public Task<IEnumerable<Chat>> GetChatsByBlogId(PagedFilter filter, Guid blogId, CancellationToken cancellationToken);
}