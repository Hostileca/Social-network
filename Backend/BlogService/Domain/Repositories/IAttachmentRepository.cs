using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Repositories;

public interface IAttachmentRepository : IRepository<Attachment>
{
    Task<Attachment> GetAttachmentByIdAndType(string id, string type, CancellationToken cancellationToken);
}