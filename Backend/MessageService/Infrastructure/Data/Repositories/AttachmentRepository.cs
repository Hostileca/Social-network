using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class AttachmentRepository(
    AppDbContext context) 
    : RepositoryBase<Attachment>(context), IAttachmentRepository;