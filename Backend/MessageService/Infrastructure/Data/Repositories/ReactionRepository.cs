using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class ReactionRepository(
    AppDbContext context) 
    : RepositoryBase<Reaction>(context), IReactionRepository;