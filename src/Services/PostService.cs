namespace Services;

using System.Threading.Tasks;
using AutoMapper;
using Repository;
using Repository.Models;

public class PostService : IPostService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public PostService(IUnitOfWork uow, IMapper mapper )
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task CreatePost(CreatePostDto post)
    {
        try
        {   
            if (post == null || post.Content.Length < 1)
                throw new Exception("Invalid Post");

            User retrivedUser = await _uow.Users.GetByIdAsync(post.UserId);
            if (retrivedUser == null)
                throw new Exception ("user who created post is not in database");

            var tempPost = _mapper.Map<PostDto>(post);
            tempPost.DateCreated = DateTime.UtcNow;    

            var newPost = _mapper.Map<Post>(tempPost);
            
            await _uow.Posts.AddAsync(newPost);
            await _uow.SaveAsync();
        }
        catch (Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not create Post", e);
        }
    }

    public async Task DeletePostById(int id)
    {
        try
        {
            var retrievedPosts = await _uow.Posts.GetByIdAsync(id);
            if (retrievedPosts == null)
                throw new Exception($"Post with {id} id does not exist");
            
            await _uow.Posts.DeleteAsync(id);
            await _uow.SaveAsync();
        }
        catch(Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("could not delete Post", e);
        }
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByPostId(int id)
    {
        try
        {
            var retrievedPost = await _uow.Posts.GetByIdAsync(id);
            if (retrievedPost == null)
                throw new Exception($"Post with id {id} does not exist");


            var comments = new List<CommentDto>();
            foreach (var comment in await _uow.Comments.GetCommentsByPostIdAsync(id) )
                comments.Add(_mapper.Map<CommentDto>(comment)); 
                        
            return comments;
        }
        catch(Exception e)
        {
            throw new Exception($"Could not Retrived Comments for PostId:{id}", e);
        }
    }

    public async Task<PostDto> GetPostByPostId(int id)
    {
        try
        {
            var retrievedPosts = await _uow.Posts.GetByIdAsync(id);
            if (retrievedPosts == null)
                throw new Exception($"Post with {id} id does not exist");

            return _mapper.Map<PostDto>(retrievedPosts);
        }
        catch (Exception e)
        {
            throw new Exception("could not get Post", e);
        }
    }

    public async Task<IEnumerable<PostDto>> GetPostsByUserId(int id) 
    {
        try
        {
            IEnumerable<Post> retrievedPosts = await _uow.Posts.GetPostsByUserIdAsync(id);
            if(retrievedPosts == null || retrievedPosts.Count() == 0)
                throw new Exception($"User with {id} id didn't created post or user doesn't exist");
           
            return retrievedPosts.Select(post => _mapper.Map<PostDto>(post)).ToList();
        }
        catch(Exception e)
        {
            throw new Exception("could not get Post", e);
        }
    }
}