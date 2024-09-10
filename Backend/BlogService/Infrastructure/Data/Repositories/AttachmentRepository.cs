using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class AttachmentRepository(
    MongoDbContext context) 
    : RepositoryBase<Attachment>(context), IAttachmentRepository;