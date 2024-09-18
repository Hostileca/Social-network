using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class ChatMemberRepository(
    AppDbContext context) 
    : RepositoryBase<ChatMember>(context), IChatMemberRepository;