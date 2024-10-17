using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Attachments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AttachmentRepository(
    MongoDbContext context) 
    : RepositoryBase<Attachment>(context), IAttachmentRepository
{
    public async Task<Attachment> GetAttachmentByIdAndType(string id, string type, CancellationToken cancellationToken)
    {
        var spec = new AttachmentByIdAndSimpleTypeSpecification(id, type);
        
        return await _dbSet.FirstOrDefaultAsync(spec.ToExpression(), cancellationToken);
    }
}