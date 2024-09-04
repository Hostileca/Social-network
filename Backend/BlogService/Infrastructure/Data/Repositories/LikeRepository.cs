using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class LikeRepository(
    MongoDbContext context) 
    : RepositoryBase<Like>(context), ILikeRepository;