using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class PostRepository(
    MongoDbContext context)
    : RepositoryBase<Post>(context), IPostRepository;