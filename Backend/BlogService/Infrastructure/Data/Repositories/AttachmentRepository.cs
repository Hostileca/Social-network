using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class AttachmentRepository(
    MongoDbContext context) 
    : RepositoryBase<Attachment>(context), IAttachmentRepository;