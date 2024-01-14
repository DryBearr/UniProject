namespace Repository;

using Repositories;

public interface IUnitOfWork : IDisposable
{
    ICommentRepository Comments { get; }
    IPostRepository Posts { get; }
    IUserRepository Users { get; }
    
    Task SaveAsync();
    Task RollBackAsync();
}