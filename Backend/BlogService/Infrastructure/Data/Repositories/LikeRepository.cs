using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class LikeRepository(
    MongoDbContext context) 
    : RepositoryBase<Like>(context), ILikeRepository;