using System.ComponentModel.DataAnnotations;


namespace Repository.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int Likes {get; set;}
        public int DisLikes {get; set;}
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"PostId: {PostId}, DateCreated: {DateCreated}";
        }
    }


}