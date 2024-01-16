namespace Services;

public interface IPostService
{
    Task CreatePost(CreatePostDto post);
    Task<PostDto> GetPostByPostId(int id);
    Task<IEnumerable<PostDto>> GetPostsByUserId(int id);
    Task DeletePostById(int id);
    Task<IEnumerable<CommentDto>> GetCommentsByPostId(int id);
}