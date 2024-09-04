using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class PostRepository(
    MongoDbContext context)
    : RepositoryBase<Post>(context), IPostRepository;