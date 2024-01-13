using Repository.Models;

namespace Repository.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
    }
}