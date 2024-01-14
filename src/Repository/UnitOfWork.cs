namespace Repository;

using Microsoft.EntityFrameworkCore.Storage;
using Repositories;


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ICommentRepository _commentRepository;
    private IPostRepository _postRepository;
    private IUserRepository _userRepository;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _transaction = _context.Database.BeginTransaction();
    }

    public ICommentRepository Comments
    {
        get { return _commentRepository ??= new CommentRepository(_context); }
    }

    public IPostRepository Posts
    {
        get { return _postRepository ??= new PostRepository(_context); }
    }

    public IUserRepository Users
    {
        get { return _userRepository ??= new UserRepository(_context); }
    }

    public async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch(Exception e)
        {
            await _transaction.RollbackAsync();
            throw new Exception("Somthing Went Wrong!", e);
        }
    }
    public async Task RollBackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
