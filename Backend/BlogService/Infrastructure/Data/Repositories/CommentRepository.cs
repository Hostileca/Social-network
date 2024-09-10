using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class CommentRepository(
    MongoDbContext context) 
    : RepositoryBase<Comment>(context), ICommentRepository;