using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Repositories;

public interface IAttachmentRepository : IRepository<Attachment>
{
    public Task<byte[]> LoadAsync(string filePath, CancellationToken cancellationToken);
}