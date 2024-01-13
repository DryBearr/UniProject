using Repository.Models;

namespace Repository.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
    }
}