using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class CommentRepository(
    MongoDbContext context) 
    : RepositoryBase<Comment>(context), ICommentRepository;