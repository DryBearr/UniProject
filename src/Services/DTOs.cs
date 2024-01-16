namespace Services;

public class UserDto
{
    public string Username {get; set;}
    public string UnhashedPassword {get; set;}
}

public class UpdateUserDto
{
    public int UserId {get; set;}
    public string Username {get; set;}
    public string UnhashedPassword {get; set;}
}

public class PostDto
{
    public int PostId {get; set;}
    public string Content {get; set;}
    public int UserId {get; set;}
    public DateTime DateCreated { get; set; }

}

public class CreatePostDto
{
    public string Content {get; set;}
    public int UserId { get; set; }
}

public class CommentDto
{
    public int? CommentId {get; set;}
    public string Content {get; set;}
    public DateTime DateCreated {get; set;}
    public int PostId {get; set;}
    public int UserId {get; set;}
}

public class ReplyCommentDto
{
    public int? CommentId {get; set;}
    public int ParentCommentId {get; set;}
    public int UserId {get; set;}
    public string Content {get; set;}
}