using System.Linq.Expressions;
using AutoMapper;
using Repository;
using Repository.Models;
namespace Services;


public class CommentService : ICommentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task CreateComment(CommentDto comment)
    {
        try
        {
            if (comment == null)
                throw new Exception("comment is null");
            
            if (comment.Content.Length < 1)
                throw new Exception("invalid comment content");
            
            var retrievedUser = await _uow.Users.GetByIdAsync(comment.UserId);
            if (retrievedUser == null)
                throw new Exception($"Comment created invalid user id:{comment.UserId}");                 

            var retrievedPost = await _uow.Posts.GetByIdAsync(comment.PostId);
            if (retrievedPost == null)
               throw new Exception($"Comment created under the invalid Post id:{comment.PostId}");

            comment.CommentId = null;
            comment.DateCreated = DateTime.UtcNow;

            var newComment = _mapper.Map<Comment>(comment);

            await _uow.Comments.AddAsync(newComment);
            await _uow.SaveAsync();
        }
        catch (Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not Create new Comment", e);        
        }
    }
    public async Task CreateReply(ReplyCommentDto reply)
    {
        try
        {
            
            if (reply == null)
                throw new Exception("reply is null");
            
            if (reply.Content.Length < 1)
                throw new Exception("invalid reply content");
            
            var retrievedParentComment = await _uow.Comments.GetByIdAsync(reply.ParentCommentId);
            if (retrievedParentComment == null)
                throw new Exception($"replied to comment with invalid Id{reply.ParentCommentId}");

            var retrievedUser = await _uow.Users.GetByIdAsync(reply.UserId);
            if (retrievedUser == null)
                throw new Exception($"reply created invalid user id:{reply.UserId}");                 

            reply.CommentId = null;
            reply.DateCreated = DateTime.UtcNow;
            var newReply = _mapper.Map<Comment>(reply);

            await _uow.Comments.AddAsync(newReply);
            await _uow.SaveAsync();
        }
        catch (Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not Create new Reply", e);        
        }
    }
    public async Task DeleteCommentByCommentId(int id)
    {
        try
        {
            var retrievedComment = await _uow.Comments.GetByIdAsync(id);
            if (retrievedComment == null)
                throw new Exception($"Comment with id:{id} does not exist");

            await _uow.Comments.DeleteAsync(id);
            await _uow.SaveAsync();

        }
        catch (Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not Delete Comment", e);        
        }
    }
    public async Task<CommentDto> GetCommentById(int id)
    {
        try
        {
            var retrievedComment = await _uow.Comments.GetByIdAsync(id);
            if (retrievedComment == null)
                throw new Exception($"Comment with id:{id} does not exist");
            
            return _mapper.Map<CommentDto>(retrievedComment);
        }
        catch (Exception e)
        {
            throw new Exception("Could not Get Comment", e);        
        }
    }
    public async Task<IEnumerable<ReplyCommentDto>> GetRepliesByParentCommentId(int id)
    {
        try
        {
            var replies = await _uow.Comments.GetRepliesByCommentIdAsync(id);
            if (replies == null)
                throw new Exception("Something went wrong");
 
            var newReplies = new List<ReplyCommentDto>();
            foreach (var reply in replies)
                newReplies.Add(_mapper.Map<ReplyCommentDto>(reply));
            
            return newReplies;
        }
        catch (Exception e)
        {
            throw new Exception("Could not Get Comment Replies", e);        
        }
    }
}