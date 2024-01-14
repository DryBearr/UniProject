using System.ComponentModel.DataAnnotations;


namespace Repository.Models
{
        public class Comment
        {
            public int CommentId { get; set; }
            [Required]
            public string Content { get; set; }
            [Required]
            public DateTime DateCreated { get; set; }
            [Required]
            public int UserId { get; set; }
            public virtual User User { get; set; }
            public int PostId { get; set; }
            public virtual Post Post { get; set; }
            public int? ParentCommentId { get; set; }
            public virtual Comment ParentComment { get; set; }
            public virtual ICollection<Comment> Replies { get; set; }
            public override string ToString()
            {
                return $"CommentId: {CommentId}, DateCreated: {DateCreated}";
            }
        }
}