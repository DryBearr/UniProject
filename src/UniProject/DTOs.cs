using System.ComponentModel.DataAnnotations;
namespace DTOs
{
    public class RequestUserDto
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string UnhashedPassword {get; set;}
    }

    public class ResponseUserDto
    {
        public int UserId {get; set;}
        public string Username {get; set;}
    }

    public class RequestPostDto
    {
        [Required]
        public int UserId {get; set;}
        [Required]
        public string Content {get; set;}

    }

}

