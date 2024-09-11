using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class ChatRepository(
    AppDbContext context) 
    : RepositoryBase<Chat>(context), IChatRepository;