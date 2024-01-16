using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await _context.Set<Comment>().Where(c => c.ParentCommentId == commentId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Set<Comment>().Where(c => c.PostId == postId).ToListAsync();
        }
    }
}