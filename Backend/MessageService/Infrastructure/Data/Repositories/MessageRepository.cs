using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class MessageRepository(
    AppDbContext context) 
    : RepositoryBase<Message>(context), IMessageRepository;