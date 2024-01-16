namespace Services;

public interface ICommentService
{
    Task CreateComment(CommentDto comment);    
    Task CreateReply(ReplyCommentDto reply);
    Task<CommentDto> GetCommentById(int id);
    Task<IEnumerable<ReplyCommentDto>> GetRepliesByParentCommentId(int id);
    Task DeleteCommentByCommentId(int id);
}